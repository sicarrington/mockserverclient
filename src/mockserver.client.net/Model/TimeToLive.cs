/* 
 * Mock Server API
 *
 * MockServer enables easy mocking of any system you integrate with via HTTP or HTTPS with clients written in Java, JavaScript and Ruby and a simple REST API (as shown below).  MockServer Proxy is a proxy that introspects all proxied traffic including encrypted SSL traffic and supports Port Forwarding, Web Proxying (i.e. HTTP proxy), HTTPS Tunneling Proxying (using HTTP CONNECT) and SOCKS Proxying (i.e. dynamic port forwarding).  Both MockServer and the MockServer Proxy record all received requests so that it is possible to verify exactly what requests have been sent by the system under test.
 *
 * OpenAPI spec version: 5.6.x
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace MockServer.Client.Net.Models
{
    /// <summary>
    /// TimeToLive
    /// </summary>
    [DataContract]
    public sealed partial class TimeToLive : IEquatable<TimeToLive>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeToLive" /> class.
        /// </summary>
        /// <param name="timeUnit">timeUnit.</param>
        /// <param name="timeToLive">timeToLive.</param>
        /// <param name="unlimited">unlimited.</param>
        public TimeToLive(string timeUnit = default(string), int? timeToLive = default(int?), bool? unlimited = default(bool?))
        {
            this.TimeUnit = timeUnit;
            this._TimeToLive = timeToLive;
            this.Unlimited = unlimited;
        }

        /// <summary>
        /// Gets or Sets TimeUnit
        /// </summary>
        [DataMember(Name = "timeUnit", EmitDefaultValue = false)]
        public string TimeUnit { get; set; }

        /// <summary>
        /// Gets or Sets _TimeToLive
        /// </summary>
        [DataMember(Name = "timeToLive", EmitDefaultValue = false)]
        public int? _TimeToLive { get; set; }

        /// <summary>
        /// Gets or Sets Unlimited
        /// </summary>
        [DataMember(Name = "unlimited", EmitDefaultValue = false)]
        public bool? Unlimited { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TimeToLive {\n");
            sb.Append("  TimeUnit: ").Append(TimeUnit).Append("\n");
            sb.Append("  _TimeToLive: ").Append(_TimeToLive).Append("\n");
            sb.Append("  Unlimited: ").Append(Unlimited).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, JsonSerializerOptionsConstants.Default);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as TimeToLive);
        }

        /// <summary>
        /// Returns true if TimeToLive instances are equal
        /// </summary>
        /// <param name="input">Instance of TimeToLive to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TimeToLive input)
        {
            if (input == null)
                return false;

            return
                (
                    this.TimeUnit == input.TimeUnit ||
                    (this.TimeUnit != null &&
                    this.TimeUnit.Equals(input.TimeUnit))
                ) &&
                (
                    this._TimeToLive == input._TimeToLive ||
                    (this._TimeToLive != null &&
                    this._TimeToLive.Equals(input._TimeToLive))
                ) &&
                (
                    this.Unlimited == input.Unlimited ||
                    (this.Unlimited != null &&
                    this.Unlimited.Equals(input.Unlimited))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.TimeUnit != null)
                    hashCode = hashCode * 59 + this.TimeUnit.GetHashCode();
                if (this._TimeToLive != null)
                    hashCode = hashCode * 59 + this._TimeToLive.GetHashCode();
                if (this.Unlimited != null)
                    hashCode = hashCode * 59 + this.Unlimited.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}