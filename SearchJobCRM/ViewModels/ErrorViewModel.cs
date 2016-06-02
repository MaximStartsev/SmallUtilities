using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using MaximStartsev.SmallUtilities.SearchJobCRM.Utilities;
using System.Collections.Generic;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels
{
    class ErrorViewModel
    {
        public List<Error> Errors = new List<Error>();
        public ErrorViewModel()
        {
            ErrorRegistrator.RegisteredError += ErrorRegistrator_RegisteredError;
        }

        private void ErrorRegistrator_RegisteredError(object sender, RegisteredErrorArgs e)
        {
            Errors.Add(e.Error);
        }
    }
}
