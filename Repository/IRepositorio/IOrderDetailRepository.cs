using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositorio
{
    public interface IOrderDetailRepository
    {
        ICollection<OrderDetail> GetAllOrderDetail();
        ICollection<OrderDetail> GetAllOrderDetailByOrderId(int orderId);
        bool CrearOrderDetail(OrderDetail orderDetail);
        bool UpdateOrderDetail(OrderDetail orderDetail);
        bool DeleteOrderDetail(OrderDetail orderDetail);

    }
}
