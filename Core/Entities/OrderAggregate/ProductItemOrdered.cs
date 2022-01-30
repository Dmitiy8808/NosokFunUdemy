using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int produtItemId, string productName, string pictureUrl)
        {
            ProdutItemId = produtItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProdutItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}