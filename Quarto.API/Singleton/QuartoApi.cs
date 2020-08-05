using System;
using Quarto.Common.Package;

namespace Quarto.API.Singleton
{
    public class QuartoApi : IAppCache
    {
        public string AppSecret { get; set; }
    }
}
