using Bsdd.Core.Logic.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsdd.Core.Logic.Services
{
    public class ServiceFactory : IServiceFactory
    {
        public IBrowserService CreateBrowserService()
        {
            return new BrowserService();
        }
        public IfcExportService CreateIfcExportService()
        {
            return new IfcExportService();
        }
    }
}
