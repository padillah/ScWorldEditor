using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLocator.WindowViewModelMapping;

namespace SCWorldEdit.WindowViewModelMapping
{
    public class ScWorldEditMapping : WindowViewModelMappings
    {
        public ScWorldEditMapping()
        {
            Mappings = new Dictionary<Type, Type>
                       {
                           { typeof(MainViewModel), typeof(string) } 
                           //,{ typeof(NewReservationViewModel), typeof(ReservationView) }
                           //,{ typeof(EditReservationViewModel), typeof(ReservationView) }
                           //,{ typeof(NewMemberViewModel), typeof(MemberView) } 
                           //,{ typeof(EditMemberViewModel), typeof(MemberView) }
                           //,{ typeof(EditAttendanceViewModel), typeof(AttendanceView) }
                           //,{ typeof(EditNoteViewModel), typeof(EditNoteView) }
                           //,{ typeof(ReservationHistoryViewModel), typeof(ReservationHistoryView) }
                           //,{ typeof(MergeMemberViewModel), typeof(MergeMemberView) }
                           //,{ typeof(EditSettingViewModel), typeof(EditSettingView) }

                        };

        }
    }
}
