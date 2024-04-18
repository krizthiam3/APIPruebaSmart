using Entities;
using PruebaAPI.Data;
using Repository.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db){_db = db;}

        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            _db.Update(orderDetail);
            return Guardar();
        }

        public bool CrearOrderDetail(OrderDetail orderDetail)
        {
            _db.Add(orderDetail);
            return Guardar();
        }
      
        public bool DeleteOrderDetail(OrderDetail orderDetail)
        {
            _db.Remove(orderDetail);
            return Guardar();
        }

        public OrderDetail GetOrderDetailById(int id)
        {
            return _db.orderDetail.FirstOrDefault(p => p.OrderDetailId == id);
        }


        public ICollection<OrderDetail> GetAllOrderDetailByOrderId(int id)
        {
            return _db.orderDetail.Where(x=>x.OrderId == id).OrderBy(p => p.OrderId).ToList();
        }


        public ICollection<OrderDetail> GetAllOrderDetail()
        {
            return _db.orderDetail.OrderBy(p => p.OrderDetailId).ToList();
        }

        private bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        private bool OrderDetailExist(int id)
        {
            bool valor = _db.orderDetail.Any(p => p.OrderDetailId == id);
            return valor;
        }

      
    }
}
