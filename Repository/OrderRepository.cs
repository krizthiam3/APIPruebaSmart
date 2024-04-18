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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db){_db = db;}

        public bool UpdateOrder(Order order)
        {
            _db.Update(order);
            return Guardar();
        }

        public bool CrearOrder(Order order)
        {
            _db.Add(order);    
            return Guardar();
        }

        public bool CrearOrderDetail(Order order)
        {
            _db.Add(order);
            return Guardar();
        }

        public bool DeleteOrder(Order order)
        {
            _db.Remove(order);
            return Guardar();
        }

        public Order GetOrderById(int id)
        {
            return _db.order.FirstOrDefault(p => p.OrderId == id);               
        }

        public ICollection<Order> GetAllOrders()
        {
            return _db.order.OrderBy(p => p.OrderId).ToList();
        }

        private bool Guardar()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        private bool OrderExist(int id)
        {
            bool valor = _db.order.Any(p => p.OrderId == id);
            return valor;
        }

      
    }
}
