using Lab08.MVC.Web.Logger;
using System;
using System.Web.Mvc;

namespace Lab08.MVC.Web.Filters
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter, IDisposable
    {
        private Log logger;
        public ExceptionLoggerAttribute()
        {
            this.logger = new Log("exception logger");
        }
        public void OnException(ExceptionContext filterContext)
        {
            logger.Logger.Error(filterContext);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                logger.Dispose();
            }
        }
    }
}