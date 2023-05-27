using System;

namespace BookStore.Entities;

public class Book
{
    public int Id { get; set; }

    ///<summary> 名称 </summary>
    public string Name { get; set; } = "";

    ///<summary> 编号 </summary>
    public string? Number { get; set; }

    ///<summary> 类型 </summary>
    public string? Type { get; set; }

    ///<summary> 价格 </summary>
    public decimal Price { get; set; }

      ///<summary> 图片 </summary>
    public string? Image { get; set; }

    ///<summary> 入库时间 </summary>
    public DateTime StorageTime { get; set; }
}