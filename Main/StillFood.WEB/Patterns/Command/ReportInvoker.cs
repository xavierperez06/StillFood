using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StillFood.WEB.Patterns.Command
{
    public class ReportInvoker
    {
        private Command _command;

        public void SetearCommand(Command pCommand)
        {
            this._command = pCommand;
        }

        public void EjecutarComando()
        {
            _command.Ejecutar();
        }

        public List<object> Resultado
        {
            get
            {
                return _command.Resultado;
            }
        }
    }
}