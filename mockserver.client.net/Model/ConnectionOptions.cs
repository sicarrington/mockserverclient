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
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace MockServer.Client.Net.Models
{
    /// <summary>
    /// ConnectionOptions
    /// </summary>
    [DataContract]
    public partial class ConnectionOptions : IEquatable<ConnectionOptions>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionOptions" /> class.
        /// </summary>
        /// <param name="closeSocket">closeSocket.</param>
        /// <param name="contentLengthHeaderOverride">contentLengthHeaderOverride.</param>
        /// <param name="suppressContentLengthHeader">suppressContentLengthHeader.</param>
        /// <param name="suppressConnectionHeader">suppressConnectionHeader.</param>
        /// <param name="keepAliveOverride">keepAliveOverride.</param>
        public ConnectionOptions(bool? closeSocket = default(bool?), int? contentLengthHeaderOverride = default(int?), bool? suppressContentLengthHeader = default(bool?), bool? suppressConnectionHeader = default(bool?), bool? keepAliveOverride = default(bool?))
        {
            this.CloseSocket = closeSocket;
            this.ContentLengthHeaderOverride = contentLengthHeaderOverride;
            this.SuppressContentLengthHeader = suppressContentLengthHeader;
            this.SuppressConnectionHeader = suppressConnectionHeader;
            this.KeepAliveOverride = keepAliveOverride;
        }

        /// <summary>
        /// Gets or Sets CloseSocket
        /// </summary>
        [DataMember(Name = "closeSocket", EmitDefaultValue = false)]
        public bool? CloseSocket { get; set; }

        /// <summary>
        /// Gets or Sets ContentLengthHeaderOverride
        /// </summary>
        [DataMember(Name = "contentLengthHeaderOverride", EmitDefaultValue = false)]
        public int? ContentLengthHeaderOverride { get; set; }

        /// <summary>
        /// Gets or Sets SuppressContentLengthHeader
        /// </summary>
        [DataMember(Name = "suppressContentLengthHeader", EmitDefaultValue = false)]
        public bool? SuppressContentLengthHeader { get; set; }

        /// <summary>
        /// Gets or Sets SuppressConnectionHeader
        /// </summary>
        [DataMember(Name = "suppressConnectionHeader", EmitDefaultValue = false)]
        public bool? SuppressConnectionHeader { get; set; }

        /// <summary>
        /// Gets or Sets KeepAliveOverride
        /// </summary>
        [DataMember(Name = "keepAliveOverride", EmitDefaultValue = false)]
        public bool? KeepAliveOverride { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ConnectionOptions {\n");
            sb.Append("  CloseSocket: ").Append(CloseSocket).Append("\n");
            sb.Append("  ContentLengthHeaderOverride: ").Append(ContentLengthHeaderOverride).Append("\n");
            sb.Append("  SuppressContentLengthHeader: ").Append(SuppressContentLengthHeader).Append("\n");
            sb.Append("  SuppressConnectionHeader: ").Append(SuppressConnectionHeader).Append("\n");
            sb.Append("  KeepAliveOverride: ").Append(KeepAliveOverride).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ConnectionOptions);
        }

        /// <summary>
        /// Returns true if ConnectionOptions instances are equal
        /// </summary>
        /// <param name="input">Instance of ConnectionOptions to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ConnectionOptions input)
        {
            if (input == null)
                return false;

            return
                (
                    this.CloseSocket == input.CloseSocket ||
                    (this.CloseSocket != null &&
                    this.CloseSocket.Equals(input.CloseSocket))
                ) &&
                (
                    this.ContentLengthHeaderOverride == input.ContentLengthHeaderOverride ||
                    (this.ContentLengthHeaderOverride != null &&
                    this.ContentLengthHeaderOverride.Equals(input.ContentLengthHeaderOverride))
                ) &&
                (
                    this.SuppressContentLengthHeader == input.SuppressContentLengthHeader ||
                    (this.SuppressContentLengthHeader != null &&
                    this.SuppressContentLengthHeader.Equals(input.SuppressContentLengthHeader))
                ) &&
                (
                    this.SuppressConnectionHeader == input.SuppressConnectionHeader ||
                    (this.SuppressConnectionHeader != null &&
                    this.SuppressConnectionHeader.Equals(input.SuppressConnectionHeader))
                ) &&
                (
                    this.KeepAliveOverride == input.KeepAliveOverride ||
                    (this.KeepAliveOverride != null &&
                    this.KeepAliveOverride.Equals(input.KeepAliveOverride))
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
                if (this.CloseSocket != null)
                    hashCode = hashCode * 59 + this.CloseSocket.GetHashCode();
                if (this.ContentLengthHeaderOverride != null)
                    hashCode = hashCode * 59 + this.ContentLengthHeaderOverride.GetHashCode();
                if (this.SuppressContentLengthHeader != null)
                    hashCode = hashCode * 59 + this.SuppressContentLengthHeader.GetHashCode();
                if (this.SuppressConnectionHeader != null)
                    hashCode = hashCode * 59 + this.SuppressConnectionHeader.GetHashCode();
                if (this.KeepAliveOverride != null)
                    hashCode = hashCode * 59 + this.KeepAliveOverride.GetHashCode();
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
