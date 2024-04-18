using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositorio
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrders();
        Order GetOrderById(int id);
        bool CrearOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);

    }
}
