using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace TimeStructures
{
    /// <summary>
    /// Struktura Time opisuje punkt w czasie, w przedziale 00:00:00 … 23:59:59.
    /// </summary>
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Konstruktor 3 argumentowy
        /// </summary>
        /// <param name="hours">0..23</param>
        /// <param name="minutes">0..59</param>
        /// <param name="seconds">0..59</param>
        public Time(byte hours, byte minutes, byte seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;

            Check(Hours, Minutes, Seconds);
        }

        /// <summary>
        /// Konstruktor 2 argumentowy
        /// </summary>
        /// <param name="hours">0..23</param>
        /// <param name="minutes">0..59</param>
        public Time(byte hours, byte minutes) : this(hours, minutes, default) { }

        /// <summary>
        /// Konstruktor 1 parametrowy
        /// </summary>
        /// <param name="hours">0..23</param>
        public Time(byte hours) : this(hours, default, default) { }

        /// <summary>
        /// Konstruktor domyslny - tworzy zmienna o godzinie 00:00:00
        /// </summary>
        public Time() : this(default, default, default) { }

        /// <summary>
        /// Konstruktor dla parametru typu string o formacie "00:00:00"
        /// </summary>
        /// <param name="time"></param>
        /// <exception cref="ArgumentNullException">Zglasza wyjatek, gdy parametr jest nullem lub gdy jest pusty</exception>
        /// <exception cref="FormatException">Zglasza wyjatek, gdy argument jest podany w innym formacie</exception>
        public Time(string time)
        {
            if (time == null || time.Equals("")) throw new ArgumentNullException();

            var regex = @"^(2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]$";
            Match match = Regex.Match(time, regex);

            if (!match.Success)
                throw new FormatException();

            var tmp = time.Split(':');

            Hours = Convert.ToByte(tmp[0]);
            Minutes = Convert.ToByte(tmp[1]);
            Seconds = Convert.ToByte(tmp[2]);
        }

        /// <summary>
        /// Propertiesy Time
        /// </summary>


        public byte Hours { get; init; }
        public byte Minutes { get; init; }
        public byte Seconds { get; init; }


        private static bool Check(byte hours, byte minutes, byte seconds)
        {
            if (hours < 0 || hours > 23
                || minutes < 0 || minutes > 59
                || seconds < 0 || seconds > 59)
                throw new ArgumentOutOfRangeException();
            return true;
        }

        /// <summary>
        /// Przeciazenie funkcji ToString()
        /// </summary>
        /// <returns>zwraca lancuch znakow w formacie "00:00:00" reprezentujacy dana zmienna</returns>
        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }

        /// <summary>
        /// Przeciazenie funkcji Equals()
        /// </summary>

        public override bool Equals(object? obj) => obj is Time other && this.Equals(other);

        public bool Equals(Time other) => other.Hours == Hours && other.Minutes == Minutes && other.Seconds == Seconds;

        /// <summary>
        /// Funkcja haszujaca
        /// </summary>

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Implementacja funkcji CompareTo
        /// </summary>

        public int CompareTo(Time other)
        {
            if (Hours != other.Hours)
                return Hours > other.Hours ? 1 : -1;
            else if (Minutes != other.Minutes)
                return Minutes > other.Minutes ? 1 : -1;
            else
                return Seconds > other.Seconds ? 1 : -1;
        }

        /// <summary>
        /// Przeciazenie operatorow ==, !=, <, >, <=, >=
        /// </summary>

        public static bool operator ==(Time left, Time right) => left.Equals(right);
        public static bool operator !=(Time left, Time right) => !(left == right);
        public static bool operator <(Time left, Time right) => left != right && left.CompareTo(right) == -1;
        public static bool operator >(Time left, Time right) => left != right && !(left < right);
        public static bool operator <=(Time left, Time right) => left < right || left == right;
        public static bool operator >=(Time left, Time right) => left > right || left == right;

        /// <summary>
        /// Tworzy nowy obiekt po dodaniu TimePeriod do istniejacej struktury
        /// </summary>
        public Time Plus(TimePeriod timePeriod)
        {
            var result = timePeriod + new TimePeriod(Hours, Minutes, Seconds);

            return new Time(Convert.ToByte(result.Hours % 24), result.Minutes, result.Seconds);
        }

        /// <summary>
        /// Funkcja statyczna tworzaca nowy obiekt Time po dodaniu do Time struktury TimePeriod
        /// </summary>
        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            return time.Plus(timePeriod);
        }

        /// <summary>
        /// Tworzy nowy obiekt po odjeciu TimePeriod od istniejacej struktury
        /// </summary>
        public Time Minus(TimePeriod timePeriod)
        {
            var tmp1 = new TimePeriod(Hours, Minutes, Seconds);
            var tmp2 = new TimePeriod(Convert.ToByte(timePeriod.Hours % 24), timePeriod.Minutes, timePeriod.Seconds);

            if (tmp1 > tmp2)
            {
                var result = tmp1 - tmp2;

                return new Time(Convert.ToByte(result.Hours % 24), result.Minutes, result.Seconds);
            }
            else
            {
                var result = new TimePeriod(86400) - (tmp2 - tmp1);

                return new Time(Convert.ToByte(result.Hours % 24), result.Minutes, result.Seconds);
            }
        }

        /// <summary>
        /// Funkcja statyczna tworzaca nowy obiekt Time po odjeciu od Time struktury TimePeriod
        /// </summary>
        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            return time.Minus(timePeriod);
        }

        /// <summary>
        /// Statyczne przeciazenie operatora +
        /// </summary>
        public static Time operator +(Time left, Time right)
        {
            return Time.Plus(left, new TimePeriod(right.Hours, right.Minutes, right.Seconds));
        }

        /// <summary>
        /// Statyczne przeciazenie operatora -
        /// </summary>
        public static Time operator -(Time left, Time right)
        {
            return Time.Minus(left, new TimePeriod(right.Hours, right.Minutes, right.Seconds));
        }

    }

    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long _seconds;

        /// <summary>
        /// Propertisy zwracajace liczbe godzin, liczbe minut i liczbe sekund w zaleznosci od pozostalych
        /// </summary>
        public byte Hours { get => Convert.ToByte(_seconds / 3600); }
        public byte Minutes { get => Convert.ToByte((_seconds / 60) % 60); }
        public byte Seconds { get => Convert.ToByte(_seconds % 60); }

        /// <summary>
        /// Propertisy zwracajace laczna liczbe godzin, minut, sekund
        /// </summary>
        public byte TotalHours { get => Convert.ToByte(_seconds / 3600); }
        public byte TotalMinutes { get => Convert.ToByte(_seconds / 60); }
        public byte TotalSeconds { get => Convert.ToByte(_seconds); }

        /// <summary>
        /// Konstruktor 3 argumentowy
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes">0..59</param>
        /// <param name="seconds">0..59</param>
        public TimePeriod(byte hours, byte minutes, byte seconds)
        {
            Check(hours, minutes, seconds);

            _seconds = hours * 3600 + minutes * 60 + seconds;
        }

        /// <summary>
        /// Konstruktor 2 argumentowy
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes">0..59</param>
        public TimePeriod(byte hours, byte minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Konstruktor 1 argumentowy
        /// </summary>
        /// <param name="seconds"></param>
        public TimePeriod(long seconds) {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException();
            _seconds  = seconds; 
        }
        /// <summary>
        /// Konstruktor domyslny - TimePeriod w postaci 0:00:00
        /// </summary>
        public TimePeriod() : this(0, 0, 0) { }

        /// <summary>
        /// Konstruktor dla parametru typu string o formacie "0:00:00"
        /// </summary>
        /// <param name="time"></param>
        /// <exception cref="ArgumentNullException">Zglasza wyjatek, gdy parametr jest nullem lub gdy jest pusty</exception>
        /// <exception cref="FormatException">Zglasza wyjatek, gdy argument jest podany w innym formacie</exception>
        public TimePeriod(string time)
        {
            if (time == null || time.Equals("")) throw new ArgumentNullException();

            var regex = @"^([0-9]|([1-9][0-9]+)):[0-5][0-9]:[0-5][0-9]$";
            Match match = Regex.Match(time, regex);

            if (!match.Success)
                throw new FormatException();

            var tmp = time.Split(':');

            _seconds = Convert.ToUInt32(tmp[0]) * 3600 + Convert.ToUInt32(tmp[1]) * 60 + Convert.ToUInt32(tmp[2]);
        }

        /// <summary>
        /// Przeciazenie funkcji ToString()
        /// </summary>
        /// <returns>zwraca lancuch znakow w formacie "0:00:00" reprezentujacy dana zmienna</returns>
        public override string ToString()
        {
            return $"{Hours}:{Minutes:D2}:{Seconds:D2}";
        }

        /// <summary>
        /// Przeciazenie funkcji Equals()
        /// </summary>
        public override bool Equals(object? obj) => obj is TimePeriod other && this.Equals(other);

        public bool Equals(TimePeriod other) => _seconds == other._seconds;

        /// <summary>
        /// Funkcja haszujaca
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Implementacja funkcji CompareTo
        /// </summary>
        public int CompareTo(TimePeriod other)
        {
            if (_seconds == other._seconds) return 0;
            else if (_seconds < other._seconds) return -1;
            else return 1;
        }

        /// <summary>
        /// Przeciazenie operatorow ==, !=, <, >, <=, >=
        /// </summary>
        public static bool operator ==(TimePeriod left, TimePeriod right) => left.Equals(right);
        public static bool operator !=(TimePeriod left, TimePeriod right) => !(left == right);
        public static bool operator <(TimePeriod left, TimePeriod right) => left != right && left.CompareTo(right) == -1;
        public static bool operator >(TimePeriod left, TimePeriod right) => left != right && !(left < right);
        public static bool operator <=(TimePeriod left, TimePeriod right) => left < right || left == right;
        public static bool operator >=(TimePeriod left, TimePeriod right) => left > right || left == right;

        /// <summary>
        /// Statyczne przeciazenie operatora +
        /// </summary>
        public static TimePeriod operator +(TimePeriod left, TimePeriod right)
        {
            return new TimePeriod(left._seconds + right._seconds);
        }

        /// <summary>
        /// Statyczne przeciazenie operatora -
        /// Wynikiem jest wartosc absolutna z roznicy
        /// </summary>
        public static TimePeriod operator -(TimePeriod left, TimePeriod right)
        {
            return new TimePeriod(Math.Abs(left._seconds - right._seconds));
        }

        /// <summary>
        /// Statyczne przeciazenie operatora *
        /// </summary>
        public static TimePeriod operator *(TimePeriod left, long right)
        {
            return new TimePeriod(left._seconds * right);
        }

        /// <summary>
        /// Statyczne przeciazenie operatora *
        /// </summary>
        public static TimePeriod operator *(long left, TimePeriod right)
        {
            return new TimePeriod(left * right._seconds);
        }

        /// <summary>
        /// Tworzy nowy obiekt TimePeriod po dodaniu struktury TimePeriod do istniejacego obiektu
        /// </summary>
        public TimePeriod Plus(TimePeriod timePeriod) => this + timePeriod;
        /// <summary>
        /// Statyczna funkcja tworzaca nowy obiekt TimePeriod po dodaniu dwoch struktor TimePeriod do siebie
        /// </summary>
        public static TimePeriod Plus(TimePeriod left, TimePeriod right) => left + right;
        /// <summary>
        /// Tworzy nowy obiekt TimePeriod po odjeciu struktury TimePeriod od istniejacego obiektu.
        /// Wynikiem jest wartosc absolutna z roznicy
        /// </summary>
        public TimePeriod Minus(TimePeriod timePeriod) => this - timePeriod;
        /// <summary>
        /// Statyczna funkcja tworzaca nowy obiekt TimePeriod po odjeciu dwoch struktor TimePeriod do siebie.
        /// Wynikiem jest wartosc absolutna z roznicy
        /// </summary>
        public static TimePeriod Minus(TimePeriod left, TimePeriod right) => left - right;
        /// <summary>
        /// Tworzy nowy obiekt TimePeriod po przemnozeniu struktury TimePeriod przez liczbe calkowita typu long
        /// </summary>
        public TimePeriod Multiply(long n) => this * n;
        /// <summary>
        /// Statyczna funkcja tworzaca nowy obiekt TimePeriod po przemnozeniu struktury TimePeriod przez liczbe calkowita typu long
        /// </summary>
        public static TimePeriod Multiply(TimePeriod left, long right) => left * right;

        private static bool Check(byte hours, byte minutes, byte seconds)
        {
            if (hours < 0 || minutes > 59 || minutes < 0 || seconds > 59 || seconds < 0)
                throw new ArgumentOutOfRangeException();
            return true;
        }
    }
}