using BizCommon_Std.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApiService.Common
{
    /// <summary>
    /// Api 내부에서 사용할 연결된 리스트 클래스
    /// </summary>
    public class ConnectedDB
    {
        private static ConnectedDB _instacne;
        public static ConnectedDB Instance
        {
            get
            {
                if (_instacne == null)
                {
                    _instacne = new ConnectedDB();
                }
                return _instacne;
            }
        }

        private List<ConnectionModel> _connectedList;

        public void SetConnection(ConnectionModel con)
        {
            if(_connectedList == null)
            {
                _connectedList = new List<ConnectionModel>();
            }
            _connectedList.Add(con);
        }

        public ConnectionModel GetConnection(ConnectionModel con)
        {
            if (_connectedList == null)
            {
                return null;
            }
            if (!_connectedList.Any(a => a.Equals(con))) return null;

            return _connectedList.Single(s => s.Equals(con));
        }

        public ConnectionModel GetConnection(int idx)
        {
            if (_connectedList == null)
            {
                return null;
            }
            var ret = _connectedList[idx];

            if (ret == null) return null;

            return ret;
        }

        public ConnectionModel GetConnection(string title)
        {
            if (_connectedList == null)
            {
                return null;
            }
            if (!_connectedList.Any(a => a.Title.Equals(title))) return null;

            return _connectedList.Single(s => s.Title.Equals(title));
        }

        public List<ConnectionModel> GetAllConnecions()
        {
            if (_connectedList == null)
            {
                return null;
            }
            return _connectedList;
        }
    }
}
