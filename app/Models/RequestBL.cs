using app.DAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace app.Controllers
{
    public class RequestBL
    {
        //.DB - מערך של פרמטרים ל
        //שהיא הבסיסית SqlParameter מכיוון שהמודל ביינדר לא מאתחל את המחלקה  dalהמערך הזה הוא מסוג מחלקה שכתבתי ב
        public DalParameter[] Params { get; set; }
        //שם הפרוצדורה
        public string StoredProcedure { get; set; }
        // DBקריאה ל
        public DataSet GetData()
        {
            if (CheckProcedure())
                return null;
            DataSet ds;
            if (Params != null && Params.Length > 0)
                ds = AppDal.SqlCommand(GetSqlParameter(), StoredProcedure);
            else
                ds = AppDal.SqlCommand(StoredProcedure);
            return ds;
        }
        //בדיקה האם הפרוצדורה המופעלת אינה פרוצדורת מערכת וכו'
        private bool CheckProcedure()
        {
            SqlParameter[] parm = new SqlParameter[1];
            int i = 0;
            parm[i] = new SqlParameter();
            parm[i].ParameterName = "@ProcedureName";
            parm[i].Direction = ParameterDirection.Input;
            parm[i].DbType = DbType.String;
            parm[i].Value = this.StoredProcedure;

            DataSet ds = AppDal.SqlCommand(parm, "spCheckProcedure");
            return ds.Tables[0].Rows.Count <= 0;
        }
        //ado.net המותאמים ל Params מתוך  SqlParameter מחזיר מערך של   
        private SqlParameter[] GetSqlParameter()
        {
            SqlParameter[] parm = new SqlParameter[this.Params.Length];
            for (int i = 0; i < this.Params.Length; i++)
            {
                parm[i] = new SqlParameter();
                parm[i].ParameterName = this.Params[i].ParameterName;
                parm[i].Direction = this.Params[i].Direction;
                parm[i].DbType = this.Params[i].DbType;
                parm[i].Value = this.Params[i].Value;
            }
            return parm;
        }
    }
}