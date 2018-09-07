using Shopping.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCustomer = Shopping.Data;

namespace Shopping.DAL.Map
{
    public class CustomerMapping
    {
        public static DBCustomer.Customer MapCustomer(CustomerDTO customerDTO)
        {

            DBCustomer.Customer result = new DBCustomer.Customer
            {
                Id = customerDTO.Id,
                Email = customerDTO.Email,
                Name = customerDTO.Name,
                Role = customerDTO.Role,
                Password = customerDTO.Password,
                Address1 = customerDTO.Address1,
                Address2 = customerDTO.Address2,
                Address3 = customerDTO.Address3
            };

            return result;
        }

        public static CustomerDTO MapCustomer(DBCustomer.Customer customerDTO)
        {

            CustomerDTO result = new CustomerDTO
            {
                Id = customerDTO.Id,
                Email = customerDTO.Email,
                Name = customerDTO.Name,
                Role = customerDTO.Role,
                Password = customerDTO.Password,
                Address1 = customerDTO.Address1,
                Address2 = customerDTO.Address2,
                Address3 = customerDTO.Address3
            };

            return result;
        }
    }
}
