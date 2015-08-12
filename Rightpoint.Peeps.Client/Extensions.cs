using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rightpoint.Peeps.Client
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            if (collection == null) return null;

            ObservableCollection<T> oc = new ObservableCollection<T>();

            foreach (T item in collection)
            {
                oc.Add(item);
            }

            return oc;
        }
    }
}
