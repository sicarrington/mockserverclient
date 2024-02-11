using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace MockServer.Client.Net.Models
{
    /// <summary>
    /// HttpResponse
    /// </summary>
    [DataContract]
    public sealed partial class HttpResponse : IEquatable<HttpResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponse" /> class.
        /// </summary>
        /// <param name="delay">delay.</param>
        /// <param name="body">body.</param>
        /// <param name="cookies">cookies.</param>
        /// <param name="connectionOptions">connectionOptions.</param>
        /// <param name="headers">headers.</param>
        /// <param name="statusCode">statusCode.</param>
        /// <param name="reasonPhrase">reasonPhrase.</param>
        public HttpResponse(Delay delay = default(Delay), string body = default(string), KeyToValue cookies = default(KeyToValue), ConnectionOptions connectionOptions = default(ConnectionOptions), KeyToMultiValue headers = default(KeyToMultiValue), int? statusCode = default(int?), string reasonPhrase = default(string))
        {
            this.Delay = delay;
            this.Body = body;
            this.Cookies = cookies;
            this.ConnectionOptions = connectionOptions;
            this.Headers = headers;
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
        }

        /// <summary>
        /// Gets or Sets Delay
        /// </summary>
        [DataMember(Name = "delay", EmitDefaultValue = false)]
        public Delay Delay { get; set; }

        /// <summary>
        /// Gets or Sets Body
        /// </summary>
        [DataMember(Name = "body", EmitDefaultValue = false)]
        public string Body { get; set; }

        /// <summary>
        /// Gets or Sets Cookies
        /// </summary>
        [DataMember(Name = "cookies", EmitDefaultValue = false)]
        public KeyToValue Cookies { get; set; }

        /// <summary>
        /// Gets or Sets ConnectionOptions
        /// </summary>
        [DataMember(Name = "connectionOptions", EmitDefaultValue = false)]
        public ConnectionOptions ConnectionOptions { get; set; }

        /// <summary>
        /// Gets or Sets Headers
        /// </summary>
        [DataMember(Name = "headers", EmitDefaultValue = false)]
        public KeyToMultiValue Headers { get; set; }

        /// <summary>
        /// Gets or Sets StatusCode
        /// </summary>
        [DataMember(Name = "statusCode", EmitDefaultValue = false)]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or Sets ReasonPhrase
        /// </summary>
        [DataMember(Name = "reasonPhrase", EmitDefaultValue = false)]
        public string ReasonPhrase { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class HttpResponse {\n");
            sb.Append("  Delay: ").Append(Delay).Append("\n");
            sb.Append("  Body: ").Append(Body).Append("\n");
            sb.Append("  Cookies: ").Append(Cookies).Append("\n");
            sb.Append("  ConnectionOptions: ").Append(ConnectionOptions).Append("\n");
            sb.Append("  Headers: ").Append(Headers).Append("\n");
            sb.Append("  StatusCode: ").Append(StatusCode).Append("\n");
            sb.Append("  ReasonPhrase: ").Append(ReasonPhrase).Append("\n");
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
            return this.Equals(input as HttpResponse);
        }

        /// <summary>
        /// Returns true if HttpResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of HttpResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(HttpResponse input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Delay == input.Delay ||
                    (this.Delay != null &&
                    this.Delay.Equals(input.Delay))
                ) &&
                (
                    this.Body == input.Body ||
                    (this.Body != null &&
                    this.Body.Equals(input.Body))
                ) &&
                (
                    this.Cookies == input.Cookies ||
                    (this.Cookies != null &&
                    this.Cookies.Equals(input.Cookies))
                ) &&
                (
                    this.ConnectionOptions == input.ConnectionOptions ||
                    (this.ConnectionOptions != null &&
                    this.ConnectionOptions.Equals(input.ConnectionOptions))
                ) &&
                (
                    this.Headers == input.Headers ||
                    (this.Headers != null &&
                    this.Headers.Equals(input.Headers))
                ) &&
                (
                    this.StatusCode == input.StatusCode ||
                    (this.StatusCode != null &&
                    this.StatusCode.Equals(input.StatusCode))
                ) &&
                (
                    this.ReasonPhrase == input.ReasonPhrase ||
                    (this.ReasonPhrase != null &&
                    this.ReasonPhrase.Equals(input.ReasonPhrase))
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
                if (this.Delay != null)
                    hashCode = hashCode * 59 + this.Delay.GetHashCode();
                if (this.Body != null)
                    hashCode = hashCode * 59 + this.Body.GetHashCode();
                if (this.Cookies != null)
                    hashCode = hashCode * 59 + this.Cookies.GetHashCode();
                if (this.ConnectionOptions != null)
                    hashCode = hashCode * 59 + this.ConnectionOptions.GetHashCode();
                if (this.Headers != null)
                    hashCode = hashCode * 59 + this.Headers.GetHashCode();
                if (this.StatusCode != null)
                    hashCode = hashCode * 59 + this.StatusCode.GetHashCode();
                if (this.ReasonPhrase != null)
                    hashCode = hashCode * 59 + this.ReasonPhrase.GetHashCode();
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
