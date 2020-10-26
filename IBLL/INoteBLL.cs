
using DTO;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IBLL
{
    /// <summary>
    /// 日志对象的业务逻辑层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INoteBLL: IDisposable
    {
        /// <summary>
        /// 将指定日期的日志转换为MemoryStream
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        MemoryStream ExportToExcelAsync(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 根据起止日期查询日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        List<NoteDTO> GetNotesByDate(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 单个日志新增
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task AddAsync(NoteDTO noteDTO);

        /// <summary>
        /// 单个日志删除(软删除)
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(NoteDTO noteDTO);

        /// <summary>
        /// 动态条件删除(软删除)
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task MaskDeleteAsync(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 单个日志删除
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task DeleteAsync(NoteDTO noteDTO);

        /// <summary>
        /// 动态条件删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 日志修改
        /// </summary>
        /// <param name="noteDTO"></param>
        /// <returns></returns>
        Task UpdateAsync(NoteDTO noteDTO);

        /// <summary>
        /// 动态查询单个日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        NoteDTO GetEntity(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 动态查询多个日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<NoteDTO> GetFilter(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 动态查询单个日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        NoteDTO GetEntity(Dictionary<string,object> dict);

        /// <summary>
        /// 动态查询多个日志
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<NoteDTO> GetFilter(Dictionary<string, object> dict);

        /// <summary>
        /// 动态查询单个日志及其作者
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        NoteDTO GetNote(Expression<Func<Note, bool>> exp);

        /// <summary>
        /// 动态查询多个日志及其作者
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        List<NoteDTO> GetNotes(Expression<Func<Note, bool>> exp);
    }
}
