using System;

namespace BC.EQCS.Domain.Incident
{
    public class TransitionMap<TCommand, TStatus> : IFormattable
        where TCommand : struct
        where TStatus : struct
    {
        public TransitionMap(TStatus preStatus, TCommand command, TStatus? postStatus = null)
        {
            PreStatus = preStatus;
            Command = command;
            PostStatus = postStatus;
        }

        public TStatus PreStatus { get; private set; }
        public TCommand Command { get; private set; }
        public TStatus? PostStatus { get; private set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format.ToLowerInvariant())
            {
                case "prestatus":
                    return PreStatus.ToString();

                case "command":
                    return Command.ToString();

                case "poststatus":
                    return PostStatus.ToString();

                default:
                    return ToString();
            }
        }

        public override string ToString()
        {
            var format = PostStatus != null
                ? "{0:PreStatus}_{0:PostStatus}"
                : "{0:PreStatus}_{0:Command}";

            return string.Format(format, this);
        }
    }
}