using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace AdyenPayment.Adyen.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResultCodeEnum
    {
        /// <summary>
        /// Enum AuthenticationFinished for value: AuthenticationFinished
        /// </summary>
        [EnumMember(Value = "AuthenticationFinished")]
        AuthenticationFinished = 0,

        /// <summary>
        /// Enum Authorised for value: Authorised
        /// </summary>
        [EnumMember(Value = "Authorised")]
        Authorised = 1,

        /// <summary>
        /// Enum Cancelled for value: Cancelled
        /// </summary>
        [EnumMember(Value = "Cancelled")]
        Cancelled = 2,

        /// <summary>
        /// Enum ChallengeShopper for value: ChallengeShopper
        /// </summary>
        [EnumMember(Value = "ChallengeShopper")]
        ChallengeShopper = 3,

        /// <summary>
        /// Enum Error for value: Error
        /// </summary>
        [EnumMember(Value = "Error")]
        Error = 4,

        /// <summary>
        /// Enum IdentifyShopper for value: IdentifyShopper
        /// </summary>
        [EnumMember(Value = "IdentifyShopper")]
        IdentifyShopper = 5,

        /// <summary>
        /// Enum Pending for value: Pending
        /// </summary>
        [EnumMember(Value = "Pending")]
        Pending = 6,

        /// <summary>
        /// Enum Received for value: Received
        /// </summary>
        [EnumMember(Value = "Received")]
        Received = 7,

        /// <summary>
        /// Enum RedirectShopper for value: RedirectShopper
        /// </summary>
        [EnumMember(Value = "RedirectShopper")]
        RedirectShopper = 8,

        /// <summary>
        /// Enum Refused for value: Refused
        /// </summary>
        [EnumMember(Value = "Refused")]
        Refused = 9,

        [EnumMember(Value = "Settled")]
        Settled = 10,
    }
}