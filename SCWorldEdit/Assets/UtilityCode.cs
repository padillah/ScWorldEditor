using System;

namespace SCWorldEdit.Assets
{
    public class ProductVersionNotSupportedException : ApplicationException
    { }
}

//    [ValueConversion(typeof(bool), typeof(DataGridSelectionMode))]
//    public class BoolSelectionModeConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            bool result;

//            if (bool.TryParse(value.ToString(), out result))
//            {
//                if (result)
//                {
//                    return DataGridSelectionMode.Extended;
//                }
//            }

//            return DataGridSelectionMode.Single;
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }
//    }

//    [ValueConversion(typeof(bool), typeof(String))]
//    public class ExtendedFilterHeaderConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            bool result;

//            if (bool.TryParse(argValue.ToString(), out result))
//            {
//                if (result)
//                {
//                    return "Extended Filter Active";
//                }
//            }

//            return String.Empty;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(Person), typeof(Visibility))]
//    public class PersonVisibilityConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            if (argValue == null)
//            {
//                return Visibility.Collapsed;
//            }

//            return Visibility.Visible;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(Person), typeof(Visibility))]
//    public class BirthdayVisibilityConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            DateTime lastWed = DateTime.Now.Date;
//            DateTime nextTues = DateTime.Now.Date;
//            DateTime birthdayThisYear;

//            if (argValue is Person)
//            {
//                Person localPerson = argValue as Person;
//                if (!localPerson.BirthDate.HasValue)
//                {
//                    return Visibility.Collapsed;
//                }

//                birthdayThisYear = new DateTime(DateTime.Now.Year, localPerson.BirthDate.Value.Month,
//                                                localPerson.BirthDate.Value.Day);
//            }
//            else
//            {
//                return Visibility.Collapsed;
//            }

//            while (lastWed.DayOfWeek != DayOfWeek.Wednesday)
//            {
//                lastWed = lastWed.AddDays(-1);
//            }

//            while (nextTues.DayOfWeek != DayOfWeek.Tuesday)
//            {
//                nextTues = nextTues.AddDays(1);
//            }

//            if ((lastWed <= birthdayThisYear) && (birthdayThisYear <= nextTues))
//            {
//                return Visibility.Visible;
//            }

//            return Visibility.Collapsed;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(bool), typeof(Visibility))]
//    public class PersonNoteVisibilityConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            if (argValue is Person)
//            {
//                Person localPerson = argValue as Person;

//                if (localPerson.HasNote)
//                {
//                    return Visibility.Visible;
//                }
//            }

//            return Visibility.Collapsed;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    //[ValueConversion(typeof(bool), typeof(Visibility))]
//    //public class BoolVisibilityHiddenConverter : IValueConverter
//    //{
//    //    #region IValueConverter Members

//    //    public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//    //    {
//    //        bool tempBoolVisibility;

//    //        if (bool.TryParse(argValue.ToString(), out tempBoolVisibility))
//    //        {
//    //            if (tempBoolVisibility)
//    //            {
//    //                return Visibility.Visible;
//    //            }
//    //        }

//    //        return Visibility.Collapsed;
//    //    }

//    //    public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//    //    {
//    //        throw new ApplicationException("Not Implemented");
//    //    }

//    //    #endregion
//    //}

//    /**/

//    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
//    public class HasNoteColorConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            bool tempBoolHasNote;
//            IApplicationSettings localSettings = Locator.Resolve<IApplicationSettings>();

//            SolidColorBrush bgBrush = new SolidColorBrush();

//            if (bool.TryParse(argValue.ToString(), out tempBoolHasNote))
//            {

//                if (tempBoolHasNote)
//                {
//                    bgBrush.Color = localSettings.MemberHasNoteBackgroundColor;
//                }
//                else
//                {
//                    bgBrush = null;
//                }
//            }
//            else
//            {
//                bgBrush = null;
//            }

//            return bgBrush;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(bool), typeof(string))]
//    public class HasNoteBoolConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            bool tempBool;

//            if (bool.TryParse(argValue.ToString(), out tempBool))
//            {
//                return tempBool ? "Y" : "N";
//            }
//            return "N";
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(Member), typeof(LinearGradientBrush))]
//    public class MemberBackgroundConverter : IValueConverter
//    {

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            IApplicationSettings localSettings = Locator.Resolve<IApplicationSettings>();
//            //ApplicationSettings LocalSettings;
//            GradientStop topStop;
//            GradientStop bottomStop;
//            GradientStop whiteStop;

//            Member tempMember = argValue as Member;
//            if (tempMember == null)
//            {
//                //throw new ApplicationException("Converter does not support non-Member objects.");
//                return null;
//            }

//            //LocalSettings = ApplicationSettings.GetAppSettings();

//            if (tempMember.IsProblem)
//            {
//                topStop = new GradientStop(localSettings.MemberProblemUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.MemberProblemUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomStop = new GradientStop(localSettings.MemberProblemUnselectedBackgroundColor, 1.0);
//            }
//            else if (tempMember.IsBanned)
//            {
//                topStop = new GradientStop(localSettings.MemberBannedUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.MemberBannedUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomStop = new GradientStop(localSettings.MemberBannedUnselectedBackgroundColor, 1.0);
//            }
//            else
//            {
//                topStop = new GradientStop(localSettings.MemberNormalUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.MemberNormalUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomStop = new GradientStop(localSettings.MemberNormalUnselectedBackgroundColor, 1.0);
//            }

//            LinearGradientBrush bgBrush = new LinearGradientBrush
//                {
//                    StartPoint = new Point(0, 0),
//                    EndPoint = new Point(0, 1),
//                    GradientStops = new GradientStopCollection
//                        {
//                            topStop,
//                            whiteStop,
//                            bottomStop
//                        }
//                };

//            return bgBrush;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }
//    }

//    /**/

//    [ValueConversion(typeof(Reservation), typeof(LinearGradientBrush))]
//    public class ReservationBackgroundConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            //ApplicationSettings LocalSettings;
//            GradientStop topColoredStop;
//            GradientStop bottomColoredStop;
//            GradientStop whiteStop;

//            IApplicationSettings localSettings = Locator.Resolve<IApplicationSettings>();

//            Reservation localReservation = argValue as Reservation;
//            if (localReservation == null)
//            {
//                //throw new ApplicationException("Converter does not support non-Reservation objects.");
//                return null;
//            }

//            if (localReservation.IsArrived)
//            {
//                //Arrived brush is LimeGreen
//                topColoredStop = new GradientStop(localSettings.ReservationArrivedUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.ReservationArrivedUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomColoredStop = new GradientStop(localSettings.ReservationArrivedUnselectedBackgroundColor, 1.0);
//            }
//            else if (localReservation.IsCanceled)
//            {
//                //Canceled brush is 
//                topColoredStop = new GradientStop(localSettings.ReservationCanceledUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.ReservationCanceledUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomColoredStop = new GradientStop(localSettings.ReservationCanceledUnselectedBackgroundColor, 1.0);
//            }
//            else if (localReservation.Member.IsBanned)
//            {
//                //Banned brush is Tomato
//                topColoredStop = new GradientStop(localSettings.MemberBannedUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.MemberBannedUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomColoredStop = new GradientStop(localSettings.MemberBannedUnselectedBackgroundColor, 1.0);
//            }
//            else if (localReservation.Member.IsProblem)
//            {
//                //Problem brush is Gold
//                topColoredStop = new GradientStop(localSettings.MemberProblemUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.MemberProblemUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomColoredStop = new GradientStop(localSettings.MemberProblemUnselectedBackgroundColor, 1.0);
//            }
//            else
//            {
//                //Regular brush is LightGray
//                topColoredStop = new GradientStop(localSettings.ReservationNormalUnselectedBackgroundColor, 0.0);
//                whiteStop = new GradientStop(localSettings.ReservationNormalUnselectedBackgroundColor.Lighten(1.7F), 0.3);
//                bottomColoredStop = new GradientStop(localSettings.ReservationNormalUnselectedBackgroundColor, 1.0);
//            }

//            LinearGradientBrush bgBrush = new LinearGradientBrush
//                {
//                    StartPoint = new Point(0, 0),
//                    EndPoint = new Point(0, 1),
//                    GradientStops =
//                        {
//                            topColoredStop, whiteStop, bottomColoredStop
//                        }
//                };

//            return bgBrush;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(Reservation), typeof(SolidColorBrush))]
//    public class ReservationForegroundConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            IApplicationSettings localSettings = Locator.Resolve<IApplicationSettings>();

//            SolidColorBrush fgBrush = new SolidColorBrush();

//            Reservation localReservation = argValue as Reservation;
//            if (localReservation == null)
//            {
//                //throw new ApplicationException("Converter does not support non-Reservation objects.");
//                return null;
//            }

//            fgBrush.Color = localSettings.ReservationNormalUnselectedTextColor;

//            if (localReservation.IsArrived)
//            {
//                fgBrush.Color = localSettings.ReservationArrivedUnselectedTextColor;
//            }
//            else if (localReservation.IsCanceled)
//            {
//                fgBrush.Color = localSettings.ReservationCanceledUnselectedTextColor;
//            }
//            else if (localReservation.Member.IsBanned)
//            {
//                fgBrush.Color = localSettings.MemberBannedUnselectedTextColor;
//            }
//            else if (localReservation.Member.IsProblem)
//            {
//                fgBrush.Color = localSettings.MemberProblemUnselectedTextColor;
//            }

//            if (localReservation.IsNew)
//            {
//                fgBrush.Color = localSettings.ReservationIsNewTextColor;
//            }

//            return fgBrush;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    [ValueConversion(typeof(Member), typeof(SolidColorBrush))]
//    public class MemberForegroundConverter : IValueConverter
//    {
//        #region IValueConverter Members

//        public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            IApplicationSettings localSettings = Locator.Resolve<IApplicationSettings>();
//            SolidColorBrush fgBrush = new SolidColorBrush();

//            Member localMember = argValue as Member;
//            if (localMember == null)
//            {
//                //throw new ApplicationException("Converter does not support non-Reservation objects.");
//                return null;
//            }

//            fgBrush.Color = localSettings.MemberNormalUnselectedTextColor;

//            if (localMember.IsBanned)
//            {
//                fgBrush.Color = localSettings.MemberBannedUnselectedTextColor;
//            }
//            else if (localMember.IsProblem)
//            {
//                fgBrush.Color = localSettings.MemberProblemUnselectedTextColor;
//            }

//            return fgBrush;
//        }

//        public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//        {
//            throw new ApplicationException("Not Implemented");
//        }

//        #endregion
//    }

//    /**/

//    //public class DebugConverter : IValueConverter
//    //{
//    //    #region IValueConverter Members

//    //    public object Convert(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//    //    {
//    //        return argValue;
//    //    }

//    //    public object ConvertBack(object argValue, Type argTargetType, object argParameter, CultureInfo argCulture)
//    //    {
//    //        throw new NotImplementedException();
//    //    }

//    //    #endregion
//    //}

//    /******************************************************************************************************************************/
//    [Serializable]
//    public class CannotMergeMembersException : ApplicationException
//    {
//    }

//    [Serializable]
//    public class PersonNotAvailableException : ApplicationException
//    {
//        public PersonNotAvailableException(Person argPerson)
//        {
//            _currentPerson = argPerson;
//        }

//        [NonSerialized]
//        private Person _currentPerson;
//        public Person CurrentPerson
//        {
//            get
//            {
//                return _currentPerson;
//            }
//        }
//    }

//    [Serializable]
//    public class SponsoredSingleWithoutCoupleException : ApplicationException
//    {
//    }

//    [Serializable]
//    public class StampCannotBeNegativeException : ApplicationException
//    {
//    }

//    [Serializable]
//    public class PartyMissingException : ApplicationException { }

//    [Serializable]
//    public class DiscountNotFoundException : ApplicationException { }

//    [Serializable]
//    public class DuplicatePINException : ApplicationException { }

//    [Serializable]
//    public class DuplicateDlException : ApplicationException
//    {
//        private string _origPersonName;

//        public string OrigPersonName
//        {
//            get
//            {
//                return _origPersonName;
//            }
//        }

//        private string _newPersonName;
//        public string NewPersonName
//        {
//            get
//            {
//                return _newPersonName;
//            }
//        }

//        public DuplicateDlException(string argOrigPersonName, string argNewPersonName)
//        {
//            _origPersonName = argOrigPersonName;
//            _newPersonName = argNewPersonName;
//        }

//    }

//    [Serializable]
//    public class SurveyAlreadyCompletedException : ApplicationException
//    {
//        [NonSerialized]
//        private DateTime _partyDate;
//        public DateTime PartyDate
//        {
//            get
//            {
//                return _partyDate;
//            }
//            set
//            {
//                _partyDate = value;
//            }
//        }

//        public SurveyAlreadyCompletedException(DateTime argPartyDate)
//        {
//            _partyDate = argPartyDate;
//        }
//    }

//    [Serializable]
//    public class PINRequiredException : ApplicationException
//    { }

//    /********************************************************************************************************************************/

//    /**
//    /// <summary>
//    /// Serializes the specified object
//    /// </summary>
//    /// <param name="toSerialize">Object to serialize.</param>
//    /// <returns>The object serialized to XAML</returns>
//    private string Serialize(object toSerialize)
//    {
//        XmlWriterSettings settings = new XmlWriterSettings();

//        // You might want to wrap these in #if DEBUG's 

//        settings.Indent = true;
//        settings.NewLineOnAttributes = true;

//        // this gets rid of the XML version 
//        settings.ConformanceLevel = ConformanceLevel.Fragment;

//        // buffer to a stringbuilder
//        StringBuilder sb = new StringBuilder();
//        XmlWriter writer = XmlWriter.Create(sb, settings);

//        // Need moar documentation on the manager, plox MSDN
//        XamlDesignerSerializationManager manager = new XamlDesignerSerializationManager(writer);
//        manager.XamlWriterMode = XamlWriterMode.Expression;

//        // its extremely rare for this to throw an exception
//        XamlWriter.Save(toSerialize, manager);

//        return sb.ToString();
//    }

//    /// <summary>
//    /// Deserializes an object from xaml.
//    /// </summary>
//    /// <param name="xamlText">The xaml text.</param>
//    /// <returns>The deserialized object</returns>
//    /// <exception cref="XmlException">Thrown if the serialized text is not well formed XML</exception>
//    /// <exception cref="XamlParseException">Thrown if unable to deserialize from xaml</exception>
//    private object Deserialize(string xamlText)
//    {
//        XmlDocument doc = new XmlDocument();

//        // may throw XmlException
//        doc.LoadXml(xamlText);

//        // may throw XamlParseException
//        return XamlReader.Load(new XmlNodeReader(doc));
//    }
//**/

//    /*
//    Extension method of Event Handlers
//    */

//    public static class ClassExtensions
//    {
//        //TODO: Does this extension method help at all? Or do I get rid of it?
//        //    public static void Raise<T>(this EventHandler<T> argHandler, object argSender, T argEvt) where T : EventArgs
//        //    {
//        //        if (argHandler != null) argHandler(argSender, argEvt);
//        //    }

//        //    public static void Raise(this EventHandler argHandler, object argSender, EventArgs argEvt)
//        //    {
//        //        if (argHandler != null) argHandler(argSender, argEvt);
//        //    }

//        //    public static bool IsAttached<T>(this Table<T> argTable, T argTableObject) where T : class
//        //    {
//        //        T TempObject = argTable.GetOriginalEntityState(argTableObject);
//        //        if (TempObject == null)
//        //        {
//        //            return false;
//        //        }

//        //        return true;
//        //    }

//        /// <summary>
//        ///   This method applies lighting to a color.
//        ///   For instance, a color that has a lighting factor of 1 applies, appears at its original value.
//        ///   A color with a lighting factor of 0.5 appears only half as bright as it was before.
//        ///   A color with a lighting factor of 1.5 appears roughly twice as bright as before.
//        ///   A color with a lightning factor of 2 appears white.
//        /// </summary>
//        /// <param name="argOriginalColor">Base color</param>
//        /// <param name="argLightFactor">
//        ///   Amount of light applied to the color
//        /// </param>
//        /// <returns>Lit color</returns>
//        /// <remarks>
//        ///   This routine is very fast. Even when using it in tight loops, I (Markus) have not been able to 
//        ///   meassure a significant amount of time spent in this routine (always less than 1ms). I was originally
//        ///   concerened about the performance of this, so I added a caching mechanism, but that slowed things down
//        ///   by 2 orders of magnitude.
//        /// </remarks>
//        public static Color Lighten(this Color argOriginalColor, float argLightFactor)
//        {
//            if (TransformationNotNeeded(argLightFactor))
//                return argOriginalColor;

//            if (RealBright(argLightFactor))
//                return Colors.White;

//            if (ShouldDarken(argLightFactor))
//                return DarkenColor(argOriginalColor, argLightFactor);

//            return LightenColor(argOriginalColor, argLightFactor);
//        }

//        private static bool TransformationNotNeeded(float argLightFactor)
//        {
//            float value = argLightFactor - 1.0f;

//            return value < 0.01f
//                   && value > -0.01f;
//        }

//        private static bool RealBright(float argLightFactor)
//        {
//            return argLightFactor >= 2.0f;
//        }

//        private static bool ShouldDarken(float argLightFactor)
//        {
//            return argLightFactor < 1.0f;
//        }

//        private static Color DarkenColor(Color argColor, float argLightFactor)
//        {
//            byte red = (byte)(argColor.R * argLightFactor);
//            byte green = (byte)(argColor.G * argLightFactor);
//            byte blue = (byte)(argColor.B * argLightFactor);

//            return Color.FromRgb(red, green, blue);
//        }

//        private static Color LightenColor(Color argColor, float argLightFactor)
//        {
//            // Lighten
//            // We do this by approaching 256 for a light factor of 2.0f
//            float fFactor2 = argLightFactor;
//            if (fFactor2 > 1.0f)
//            {
//                fFactor2 -= 1.0f;
//            }

//            byte red = LightenColorComponent(argColor.R, fFactor2);
//            byte green = LightenColorComponent(argColor.G, fFactor2);
//            byte blue = LightenColorComponent(argColor.B, fFactor2);

//            return Color.FromRgb(red, green, blue);
//        }

//        private static byte LightenColorComponent(byte argColorComponent, float argFFactor)
//        {
//            int inverse = 255 - argColorComponent;
//            argColorComponent += (byte)(inverse * argFFactor);

//            return argColorComponent < 255
//                       ? argColorComponent
//                       : (byte)255;
//        }
//    }


//}