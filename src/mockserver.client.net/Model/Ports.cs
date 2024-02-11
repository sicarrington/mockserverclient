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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace MockServer.Client.Net.Models
{
    /// <summary>
    /// Ports
    /// </summary>
    [DataContract]
    public sealed partial class Ports : IEquatable<Ports>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ports" /> class.
        /// </summary>
        /// <param name="ports">ports.</param>
        public Ports(List<decimal?> ports = default(List<decimal?>))
        {
            this._Ports = ports;
        }

        /// <summary>
        /// Gets or Sets _Ports
        /// </summary>
        [DataMember(Name = "ports", EmitDefaultValue = false)]
        public List<decimal?> _Ports { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Ports {\n");
            sb.Append("  _Ports: ").Append(_Ports).Append("\n");
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
            return this.Equals(input as Ports);
        }

        /// <summary>
        /// Returns true if Ports instances are equal
        /// </summary>
        /// <param name="input">Instance of Ports to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Ports input)
        {
            if (input == null)
                return false;

            return
                (
                    this._Ports == input._Ports ||
                    this._Ports != null &&
                    input._Ports != null &&
                    this._Ports.SequenceEqual(input._Ports)
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
                if (this._Ports != null)
                    hashCode = hashCode * 59 + this._Ports.GetHashCode();
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
