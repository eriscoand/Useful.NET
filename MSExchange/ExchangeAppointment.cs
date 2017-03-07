using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

class ExchangeAppointment
{
    private static string url = System.Web.Configuration.WebConfigurationManager.AppSettings["MSExchange_URL"];

    private static bool RedirectionUrlValidationCallback(string redirectionUrl)
    {
        bool result = false;

        Uri redirectionUri = new Uri(redirectionUrl);
        if (redirectionUri.Scheme == "https")
        {
            result = true;
        }
        return result;
    }

    private static ExchangeService getService(string _user, string _password, bool LDAPCredentials = true, bool autoDiscoverUrl = true)
    {
        ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
        if (LDAPCredentials)
        {
            service.UseDefaultCredentials = true;
        }
        else
        {
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials(_user, _password);
        }
        if (autoDiscoverUrl)
        {
            service.AutodiscoverUrl(_user, RedirectionUrlValidationCallback);
        }
        else
        {
            service.Url = new Uri(url);
        }
        return service;
    }

    /// <summary>
    /// This metthod connects to MSExchange and returns a list of Appointments between two dates
    /// </summary>
    /// <param name="_user">Exchange user</param>
    /// <param name="_password">Exchange password for user</param>
    /// <param name="_date_init">Initial Date</param>
    /// <param name="_date_end">Final Date</param>
    /// <param name="LDAPCredentials">Use default credentials? True by default</param>
    /// <param name="autoDiscoverUrl">Auto discover https? True by default</param>
    /// <returns>List of MSExchange Appointments</returns>
    public static List<Appointment> getAppointments(string _user, string _password, DateTime _date_init, DateTime _date_end, bool LDAPCredentials = true, bool autoDiscoverUrl = true)
    {
        ExchangeService service = getService(_user, _password, LDAPCredentials, autoDiscoverUrl);

        const int NUM_APPTS = 30;
        CalendarFolder calendar = CalendarFolder.Bind(service, WellKnownFolderName.Calendar, new PropertySet());
        CalendarView cView = new CalendarView(_date_init, _date_end, NUM_APPTS);
        cView.PropertySet = new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End);

        FindItemsResults<Appointment> appointments = calendar.FindAppointments(cView);

        return appointments.ToList();
    }

    /// <summary>
    /// This metthod connects to MSExchange and adds an appointment to the user calendar
    /// </summary>
    /// <param name="_user">Exchange user</param>
    /// <param name="_password">Exchange password for user</param>
    /// <param name="_subject">Annotation subject</param>
    /// <param name="_body">Annotation body (HTML if you want)</param>
    /// <param name="_location">Annotation location</param>
    /// <param name="_start">Initial Date</param>
    /// <param name="_end">Final Date</param>
    /// <param name="_reminder">By default false. True if you want to create a reminder</param>
    /// <param name="_minutesReminder">By default 0. Add minutes to create a reminder before the appointment's start date</param>
    /// <param name="LDAPCredentials">Use default credentials? True by default</param>
    /// <param name="autoDiscoverUrl">Auto discover https? True by default</param>
    /// <returns>MSExchangeKeys - Guid keys created by MSExchange. Save it to your DB</returns>
    public static MSExchangeKeys addAppointment(string _user, string _password, string _subject, string _body, string _location, DateTime _start, DateTime _end, bool _reminder = false, int _minutesReminder = 0, bool LDAPCredentials = true, bool autoDiscoverUrl = true)
    {
        ExchangeService service = getService(_user, _password, LDAPCredentials, autoDiscoverUrl);

        Appointment appointment = new Appointment(service);
        appointment.Subject = _subject;
        appointment.Body = new MessageBody(BodyType.HTML, _body);
        appointment.Start = _start;
        appointment.End = _end;

        if (!String.IsNullOrWhiteSpace(_location))
        {
            appointment.Location = _location;
        }

        if (_reminder)
        {
            appointment.ReminderDueBy = _start.AddMinutes(-1 * _minutesReminder);
        }

        appointment.Save(SendInvitationsMode.SendToNone);

        //Verify appointment has been created
        Item item = Item.Bind(service, appointment.Id, new PropertySet(ItemSchema.Subject));
        if (item != null)
            return new MSExchangeKeys(appointment.Id.ChangeKey, appointment.Id.UniqueId);
        else 
            return null;
    }

    /// <summary>
    /// This metthod connects to MSExchange and delete and appointment by his uniqueID
    /// </summary>
    /// <param name="_user">Exchange user</param>
    /// <param name="_password">Exchange password for user</param>
    /// <param name="uniqueID">MS Exchange Appointment Id</param>
    /// <param name="LDAPCredentials">Use default credentials? True by default</param>
    /// <param name="autoDiscoverUrl">Auto discover https? True by default</param>
    /// <returns>True if everything is OK</returns>
    public static bool deleteAppointment(string _user, string _password, string uniqueID, bool LDAPCredentials = true, bool autoDiscoverUrl = true)
    {
        try
        {            
            ExchangeService service = getService(_user, _password, LDAPCredentials, autoDiscoverUrl);
            Appointment appointment = Appointment.Bind(service, new ItemId(uniqueID), new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End));

            Item item = Item.Bind(service, appointment.Id, new PropertySet(ItemSchema.Subject));
            item.Delete(DeleteMode.HardDelete);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
