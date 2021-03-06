/*<FILE_LICENSE>
* NFX (.NET Framework Extension) Unistack Library
* Copyright 2003-2014 IT Adapter Inc / 2015 Aum Code LLC
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
</FILE_LICENSE>*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFX.Instrumentation;

namespace NFX.Log.Instrumentation
{

    [Serializable]
    public abstract class LogLongGauge : LongGauge, IInstrumentationInstrument
    {
      protected LogLongGauge(string source,long value) : base(source, value) { }
    }

    [Serializable]
    public class LogMsgCount : LogLongGauge, IMemoryInstrument
    {
        protected LogMsgCount(string source, long value) : base(source, value) {}

        public static void Record(string source, long value)
        {
           var inst = App.Instrumentation;
           if (inst.Enabled)
             inst.Record(new LogMsgCount(source, value));
        }


        public override string Description { get{ return "Log message count"; }}
        public override string ValueUnitName { get{ return CoreConsts.UNIT_NAME_MESSAGE; }}


        protected override Datum MakeAggregateInstance()
        {
            return new LogMsgCount(this.Source, 0);
        }
    }


}
