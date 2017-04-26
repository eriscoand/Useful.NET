using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.ViewModels
{
    public class Return
    {
        public bool done { get; set; }
        public string message { get; set; }

        public Return()
        {
            this.done = true;
            this.message = "";
        }

        public Return(bool done)
        {
            this.done = done;
            this.message = "";
        }

        public Return(bool done, string message)
        {
            this.done = done;
            this.message = message;
        }

    }
}
