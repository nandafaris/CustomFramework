using System.Collections.Generic;
using System.Data.Common;

namespace CustomFramework
{
    public class BaseDataProvider : BaseDataAccess
    {
        public BaseDataProvider(string connectionName) : base(connectionName)
        {
        }
        public string Query { get; set; }
        public List<object> GetList()
        {                     
            List<dynamic> results = null;
            ListItem listItem = null;
            List<DbParameter> paramList = new List<DbParameter>();
            using(DbDataReader dr = base.GetDataReader(Query, paramList, System.Data.CommandType.Text))
            {
                if (dr != null && dr.HasRows)
                {
                    results = new List<dynamic>();
                    while (dr.Read())
                    {
                        var listItemData = new List<dynamic>();                     
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            listItem = new ListItem();
                            listItem.Key = dr.GetName(i);
                            listItem.Value = dr[dr.GetName(i)].ToString();
                            listItemData.Add(listItem);
                        }
                        results.Add(listItemData);                        
                    }
                }
            }
            return results;
        }
        public List<object> GetLists()
        {
            List<dynamic> results = null;
            Dictionary<string, object> listItem = null;
            List<DbParameter> paramList = new List<DbParameter>();
            using (DbDataReader dr = base.GetDataReader(Query, paramList, System.Data.CommandType.Text))
            {
                if (dr != null && dr.HasRows)
                {
                    results = new List<dynamic>();
                    while (dr.Read())
                    {
                        var listItemData = new List<dynamic>();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {                            
                            listItem = new Dictionary<string, object>();
                            listItem.Add(dr.GetName(i), dr[dr.GetName(i)].ToString());
                            listItemData.Add(listItem);                            
                        }
                        results.Add(listItemData);
                    }
                }
            }
            return results;
        }
    }
    public class ListItem
    {
        public object Key { get; internal set; }
        public object Value { get; internal set; }
    }

}
