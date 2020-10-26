using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using CommonHelper;
using DTO;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL
{
    public class NoteBLL:INoteBLL
    {
        public IGeneralDAL generalDAL { get; set; }

        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispose()
        {
            if (generalDAL != null)
            {
                generalDAL.Dispose();
            }
        }

        /// <summary>
        /// 将指定日期的日志转换为MemoryStream
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public MemoryStream ExportToExcelAsync(DateTime startTime,DateTime endTime)
        {
            var list=generalDAL.GetFilter<Note>(e=>e.CreateTime.CompareTo(startTime)>=0&&e.CreateTime.CompareTo(endTime)<=0&&e.IsDeleted==false).ToList();
            return ExcelHelper.ExportToExcel(list);
        }

        /// <summary>
        /// 根据起止日期查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<NoteDTO> GetNotesByDate(DateTime startTime,DateTime endTime)
        {
            return generalDAL.GetFilter<Note>(e => e.CreateTime.CompareTo(startTime) >= 0 && e.CreateTime.CompareTo(endTime) <= 0 && e.IsDeleted == false).ProjectTo<NoteDTO>().ToList();
        }

        /// <summary>
        /// 单个日志新增
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task AddAsync(NoteDTO noteDTO)
        {
            //数据库中对应的日志
            var db_note = generalDAL.GetEntity<Note>(e => e.Id == noteDTO.Id && e.IsDeleted == false);
            if (db_note == null)
            {
                //要新增的数据
                var note = Mapper.Map<Note>(noteDTO);
                note.Id = Guid.NewGuid().ToString("n");
                note.CreateTime = DateTime.Now;
                note.IsDeleted = false;
                generalDAL.Add(note);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个日志删除(软删除)
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(NoteDTO noteDTO)
        {
            //数据库中的数据
            var db_note = generalDAL.GetEntity<Note>(e=>e.Id==noteDTO.Id&&e.IsDeleted==false);
            //如果数据库中有该数据，则删除
            if (db_note!=null)
            {
                generalDAL.MarkRemove(db_note);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task MaskDeleteAsync(Expression<Func<Note, bool>> exp)
        {
            //数据库中的数据
            var db_notes = generalDAL.GetFilter(exp);
            if (db_notes.Any())
            {
                generalDAL.MarkRemoveRange(db_notes);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个日志删除(只是删除)
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task DeleteAsync(NoteDTO noteDTO)
        {
            //数据库中的数据
            var db_note = generalDAL.GetEntity<Note>(e => e.Id == noteDTO.Id && e.IsDeleted == false);
            if (db_note != null)
            {
                generalDAL.Remove(db_note);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件删除(真实删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Note, bool>> exp)
        {
            var db_notes = generalDAL.GetFilter(exp);
            if (db_notes.Any())
            {
                generalDAL.RemoveRange<Note>(db_notes);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 单个日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        public async Task UpdateAsync(NoteDTO noteDTO)
        {
            //数据库表中对应的数据
            var db_note = generalDAL.GetEntity<Note>(e=>e.Id== noteDTO.Id&&e.IsDeleted==false);
            if (db_note != null)
            {
                var note = Mapper.Map<Note>(noteDTO);
                generalDAL.Update(note);
                await generalDAL.CommitAsync();
            }
        }

        /// <summary>
        /// 动态条件单个获取日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public NoteDTO GetEntity(Expression<Func<Note, bool>> exp)
        {
            var entity = generalDAL.GetEntity(exp);
            return Mapper.Map<NoteDTO>(entity);
        }

        /// <summary>
        /// 动态条件多个获取日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<NoteDTO> GetFilter(Expression<Func<Note, bool>> exp)
        {
            return generalDAL.GetFilter(exp).ProjectTo<NoteDTO>().ToList();
        }

        /// <summary>
        /// 动态查询单个日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public NoteDTO GetEntity(Dictionary<string, object> dict)
        {
            var exp = Tool<Note>.ToExpression(dict);
            var entity = generalDAL.GetEntity(exp);
            return Mapper.Map<NoteDTO>(entity);
        }

        /// <summary>
        /// 动态查询多个日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<NoteDTO> GetFilter(Dictionary<string, object> dict)
        {
            var exp = Tool<Note>.ToExpression(dict);
            return generalDAL.GetFilter(exp).ProjectTo<NoteDTO>().ToList();
        }

        /// <summary>
        /// 动态查询单个日志及其作者
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public NoteDTO GetNote(Expression<Func<Note, bool>> exp)
        {
            var db_note=generalDAL.GetEntity(exp);
            var noteDTO= Mapper.Map<NoteDTO>(db_note);
            noteDTO.Author=db_note.User.Name;
            return noteDTO;
        }

        /// <summary>
        /// 动态查询多个日志及其作者
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public List<NoteDTO> GetNotes(Expression<Func<Note, bool>> exp)
        {
            var db_notes= generalDAL.GetFilter(exp);
            var db_users = generalDAL.GetFilter<User>(e=>true);
            return (from db_note in db_notes
                    join db_user in db_users on db_note.UserId equals db_user.Id
                    select new NoteDTO()
                    {
                        Id = db_note.Id,
                        Name = db_note.Name,
                        Detail = db_note.Detail,
                        CreateTime = db_note.CreateTime,
                        IsDeleted = db_note.IsDeleted,
                        UserId = db_note.UserId,
                        Author = db_user.Name
                    }).ToList();
        }
    }
}
