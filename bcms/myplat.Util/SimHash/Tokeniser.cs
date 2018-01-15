using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    public class FixedSizeStringTokeniser : ITokeniser
    {
        private readonly ushort tokensize = 5;
        public FixedSizeStringTokeniser(ushort tokenSize)
        {
            if (tokenSize < 2 || tokenSize > 127)
            {
                throw new ArgumentException("Token 不能超出范围");
            }
            this.tokensize = tokenSize;
        }

        public IEnumerable<string> Tokenise(string input)
        {
            var chunks = new List<string>();
            int offset = 0;
            while (offset < input.Length)
            {
                chunks.Add(new string(input.Skip(offset).Take(this.tokensize).ToArray()));
                offset += this.tokensize;
            }
            return chunks;
        }

    }


    public class OverlappingStringTokeniser : ITokeniser
    {

        private readonly ushort chunkSize = 4;
        private readonly ushort overlapSize = 3;

        public OverlappingStringTokeniser(ushort chunkSize, ushort overlapSize)
        {
            if (chunkSize <= overlapSize)
            {
                throw new ArgumentException("Chunck 必须大于 overlap");
            }
            this.overlapSize = overlapSize;
            this.chunkSize = chunkSize;
        }

        public IEnumerable<string> Tokenise(string input)
        {
            var result = new List<string>();
            int position = 0;
            while (position < input.Length - this.chunkSize)
            {
                result.Add(input.Substring(position, this.chunkSize));
                position += this.chunkSize - this.overlapSize;
            }
            return result;
        }


    }
}
