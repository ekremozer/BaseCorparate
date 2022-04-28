using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseCorporate.Dal.EntityFramework
{
    public static class EfHelper
    {
        public static bool CommitOrRollback(this IDbContextTransaction transaction)
        {
            try
            {
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
