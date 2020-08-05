using System;
using Quarto.Common.Package;

namespace Quarto.Api.Singleton
{
    public class QuartoApi : IAppCache
    {
        public string AppSecret { get; set; }
    }
}
