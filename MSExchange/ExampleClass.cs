using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace Mutuand.Helpers.MSExchange
{
    class ExampleClass
    {

        private void test(){

            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = DateTime.Now.AddDays(2);
            var appointmentList = ExchangeAppointment.getAppointments("username", "password", start, end);

            Appointment app = new Appointment();
            app.Start = start;
            app.End = end;
            app

            var mskeys = ExchangeAppointment.addAppointment("username","password",

        }

    }
}
