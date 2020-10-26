using Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.IO;

namespace CommonHelper
{
    public static class ExcelHelper
    {

        /// <summary>
        /// List
        /// </summary>
        /// <param name="list"></param>
        public static MemoryStream ExportToExcel(List<Note> list)
        {
            //创建Excel文件的对象
            HSSFWorkbook book = new HSSFWorkbook();
            //添加一个sheet
            ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("Id");
            row1.CreateCell(1).SetCellValue("日志名字");
            row1.CreateCell(2).SetCellValue("日志内容");
            row1.CreateCell(3).SetCellValue("创建时间");
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(list[i].Id);
                rowtemp.CreateCell(1).SetCellValue(list[i].Name);
                rowtemp.CreateCell(2).SetCellValue(list[i].Detail);
                rowtemp.CreateCell(3).SetCellValue(list[i].CreateTime.ToString());
            }
            // 写入到客户端 
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
