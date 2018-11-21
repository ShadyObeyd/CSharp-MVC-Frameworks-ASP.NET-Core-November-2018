using System;
using System.Globalization;

namespace Eventures.App.ViewModels.Events
{
    public class AllEventsViewModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Place { get; set; }

        public string StartToStr => this.Start.ToString("dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture);

        public string EndToStr => this.End.ToString("dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture);
    }
}
