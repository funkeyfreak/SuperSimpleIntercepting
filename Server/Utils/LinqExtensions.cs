namespace Server{
    using System;
    using System.Collections.Generic;
    public static class LinqExtensions {
        public static SomeSchema ToSomeSchema<TSource,TElement> (this System.Collections.Generic.IEnumerable<TSource> source, Func<TSource,string> keySelector, Func<TSource,TElement> elementSelector)
        {
            SomeSchema s = new SomeSchema();
            foreach( var t in source){
                string? key = keySelector(t) as string;
                Console.WriteLine("adding " + key + " to the SomeSchema data set");

                switch(key)
                {
                    case nameof(SomeSchema.ElapsedSeconds):
                        s.ElapsedSeconds = elementSelector(t) as long?;
                        break;
                    case nameof(SomeSchema.SaintNick):
                        s.SaintNick = elementSelector(t) as string;
                        break;
                    case "{"+nameof(SomeSchema.OriginalFormat)+"}":
                        s.OriginalFormat = elementSelector(t) as string;
                        break;
                    default:
                        Console.WriteLine("noooooooo " + key);
                        continue;
                }
            }
            
            return s;
        }
    }
}