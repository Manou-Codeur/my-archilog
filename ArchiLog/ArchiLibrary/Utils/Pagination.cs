namespace ArchiLibrary.Pagination
{
    public class PaginationUtil
    {
        public PaginationUtil(int totalCount, int toTake, int start, int end, string Tmodel)
        {
            _count = totalCount;
            _toTake = toTake;
            _start = start;
            _end = end;
            _tModel = Tmodel;

            first = getFirst();
            last = getLast();
            next = getNext();
            prev = getPrev();

        }

        public string first;
        public string prev;
        public string next;
        public string last;
        private readonly int _count;
        private readonly int _toTake;
        private readonly int _start;
        private readonly int _end;
        private readonly string _tModel;

        string getFirst()
        {
            var range = $"1-{_toTake}";
            return $"https://localhost:7157/api/{_tModel}?Range={range}";
        }

        string getPrev()
        {
            if (_start <= 1)
            {
                return null;
            }
            var range = $"{_start - _toTake} - {_start - 1}";
            return $"https://localhost:7157/api/{_tModel}?Range={range}";
        }

        string getNext()
        {
            var range = $"{_end + 1}-{_end + _toTake}";
            return $"https://localhost:7157/api/{_tModel}?Range={range}";
        }

        string getLast()
        {
            var range = $"{_count - _toTake + 1}-{_count}";
            return $"https://localhost:7157/api/{_tModel}?Range={range}";
        }
    }
}
