using Core.Model;
using Core.Model.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository.IRepository
{
    public interface IRTUAdressRepository
    {
        List<RTUAdress> GetRTUAdresses();
        RTUAdress GetRTUAdress(string value);
        void AddRTUAdress(RTUAdress adress);
        bool DeleteRTUAdress(string value);
        RTUAdress UpdateRTUAdress(RTUAdress adress);
    }
}
