using Microsoft.Extensions.Logging;
using System;

namespace web.Logger
{
    public class AppLogger : ILogger
    {
        private readonly string _nomeCategoria;
        private readonly Func<string, LogLevel, bool> _filtro;
        private readonly RepositorioLogger _repositorio;
        private readonly int _messageMaxLength = 4000;

        public AppLogger(string nomeCategoria, Func<string, LogLevel, bool> filtro, string connectionString)
        {
            _nomeCategoria = nomeCategoria;
            _filtro = filtro;
            _repositorio = new RepositorioLogger(connectionString);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventoId,
            TState state, Exception exception, Func<TState, Exception, string> formato)
        {
            try
            {
                if (!IsEnabled(logLevel))
                {
                    return;
                }

                if (formato == null)
                {
                    throw new ArgumentNullException(nameof(formato));
                }

                var mensagem = formato?.Invoke(state, exception);
                if (string.IsNullOrEmpty(mensagem))
                {
                    return;
                }

                if (exception != null)
                {
                    mensagem += $"\n{exception.ToString()}";
                }

                mensagem = mensagem.Length > _messageMaxLength ? mensagem.Substring(0, _messageMaxLength) : mensagem;
                var eventLog = new LogEvento
                {
                    Message = mensagem,
                    EventId = eventoId.Id,
                    LogLevel = logLevel.ToString(),
                    CreatedTime = DateTime.UtcNow
                };

                _repositorio.InsertLog(eventLog);
            }
            finally
            {

            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filtro == null || _filtro(_nomeCategoria, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
