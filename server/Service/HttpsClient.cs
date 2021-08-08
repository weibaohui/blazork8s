using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace server.Service
{
    public class HttpsClient
    {
        private string _certPem = @"-----BEGIN CERTIFICATE-----
MIIDIzCCAgugAwIBAgIId1Fqtpx3M3wwDQYJKoZIhvcNAQELBQAwFTETMBEGA1UE
AxMKa3ViZXJuZXRlczAeFw0yMTA3MTgwMzE4MTVaFw0yMjA4MDgxMzU3MjJaMDYx
FzAVBgNVBAoTDnN5c3RlbTptYXN0ZXJzMRswGQYDVQQDExJkb2NrZXItZm9yLWRl
c2t0b3AwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDIFaLKK7FbjWt1
UPrq89AazUcd7DkPHuHaYGgxuqu7d0mGvEjmBjQL2jbJ5dTrj46/w0IBwyvAeER7
9+HJyIMRN23PPY7gAludhni3kyqzNpL+/Bg1JUq3snE4hSADtwDW5HRf2C7d0OAS
Il5VqkOjNnIDufQbaLfRhiLixnqfAY1XfhhJRBIcFYpbFtbzQHvrG5rLXziosy3F
g2madpIayi29n9CzNWub1jj54nOOuMSR/1iChBEV93cmVsI9VRkf4NifrHeRxbee
29YXP9+sF1/b1efVy5pxBh3s3qhfzGkqWdaFD5U2FJRHy4emOWhe/kLw4rRRCY5a
vIJX0EjFAgMBAAGjVjBUMA4GA1UdDwEB/wQEAwIFoDATBgNVHSUEDDAKBggrBgEF
BQcDAjAMBgNVHRMBAf8EAjAAMB8GA1UdIwQYMBaAFKsgcCkDpMFTLinIFvYc6TzW
fk4tMA0GCSqGSIb3DQEBCwUAA4IBAQC8XKCx6BR807P1oM91SP3iICnydn8H3I54
RLiZKnjD9uZCT4RZZf6PT/+5Nf431KPqEojFC5Y+Nv8QAkNVMWYuQkZj4V+g2My5
1Y9IpXVMGNhBQQ86IYxHvOMmNfEzbQT/AF6MHYj2TjJS0/viPAf6ZBb3u8opC6WX
T5JUMkgjJzYQgauIbksCWlCzPH1jK3Gbwi+uu+rVFHOea8W/wsNDOO5eqkl0sCUp
8Op0U76KPGrB6VFl9pr07gV9RET6GKus/0lXsFfLy74oneNlm0NE3thfsEhb7UsR
O/yk7QaKvqpDGKNHTneRiyUnxNHHNudcf4f69MUV4unkRNsojWPu
-----END CERTIFICATE-----";

        private string _keyPem = @"-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAyBWiyiuxW41rdVD66vPQGs1HHew5Dx7h2mBoMbqru3dJhrxI
5gY0C9o2yeXU64+Ov8NCAcMrwHhEe/fhyciDETdtzz2O4AJbnYZ4t5MqszaS/vwY
NSVKt7JxOIUgA7cA1uR0X9gu3dDgEiJeVapDozZyA7n0G2i30YYi4sZ6nwGNV34Y
SUQSHBWKWxbW80B76xuay184qLMtxYNpmnaSGsotvZ/QszVrm9Y4+eJzjrjEkf9Y
goQRFfd3JlbCPVUZH+DYn6x3kcW3ntvWFz/frBdf29Xn1cuacQYd7N6oX8xpKlnW
hQ+VNhSUR8uHpjloXv5C8OK0UQmOWryCV9BIxQIDAQABAoIBAGHNiaRvVSOebi+L
ZQuiIFBplWDADmggvV/Ejkn4qGdbeNpegPfHgntksDFiogB/TNTZuhModuN/JmK+
mWTMGZ05zfhma8d9/5BxoeKCEnZFr/bp/V9FGk/O7t8k27BVLRjd1TDgeJA+wLi8
igYv0x4dx/+gTv1y45+MhNL7GAdOmfwk2jHnKXhJZLkl1AHHDHuTpN2Ts7Ydyt+9
8odMI23mNqFm05lFNmvroynkDZQMPCCrCwNlnzjmeA6AAdNK7UPkvN6HKgJ/3CP7
6hSEakxhtCctPruCjLU4SPQV92f+4JvD+InXzk8OxOjaimNYljfGofr0iVgAJj8r
YXZDA+ECgYEA0VCX43FNql3f/L6NrJcJzk1m9rfR7U7mrahUttPDMFmO3crELV5o
31Q9TYEQgaWyVI2PFzEAImjh5Dwm0osSr1jmcGUHQICupLqJ+lNDaFrTvrmTVkwQ
3lSYaOmtRCiPz2kPOtH6g47yIBo4ywrHrEOtpoqfb07f6xrlSIvwjy0CgYEA9LYD
VPRWLXF0SNONHKZ97RoCN271nCTUJ/lgAJO+PHWUz2eCvkTdrwZM39MQTAjtgOEy
F/vukBA7+u/wwTuVTVRucS9mwlVH1NMzuRBnFDYiQvtU2GHqK0+vPfSqW/YKsLuW
y2B7XSW7Q1CB46U5zKlkhvAwEnm1PCrmC2Tc3vkCgYBQWPSFDSg3/qMNfQQbPTs5
YwLkL/m4c3IfNR+XssAZXjd2MfCOTdBJ8ic6ChIRAk7rIA/OYjPgAYy8tM5eQZ5h
iumiOVXIT906RJTUb4PPmhXv/4JsPPS4s5Zxp0mogT1666Q5+wKD36pX4ljsr+2n
1a7h4BKFgqx3rjJXX8hu3QKBgQDs3bFj7g3sdsEkLQGOFeoWMvKqTZEXzt48wzmV
1WxygS3FhGAdY/NgkyyeCmLf9lROR6yBYq6Ma+pi/xV/NPlTnMI6dLN2r62T58tM
+v+LpMqNI1IBEOXlfoAV7FlqIkI+x5UyzvUaOIeGpMrVWv0TiXNw2dTuTTiMQMnj
hYH1oQKBgCsV9G82FD1gOOvew7Mi2BeqIExy5NbrcFGTGyRL2XjftdEDRs3af8GP
3Iu8TzuJcnDqv7IJHBkSKOL1cExGMCgXpxRIQdBSAj6Uqv4Ohy5Jdb6oqP9hkZ/D
dvPzNb8/Y8kl9b00/JZTl+oNILl8ltjdyYAw3FiLeHBC4Yf8aePd
-----END RSA PRIVATE KEY-----";

        public HttpsClient SetCertPem(string certPem)
        {
            _certPem = certPem;
            return this;
        }

        public HttpsClient SetKeyPem(string keyPem)
        {
            _keyPem = keyPem;
            return this;
        }

        public HttpClient Build()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions                  = ClientCertificateOption.Manual,
                SslProtocols                              = SslProtocols.Tls12,
                ServerCertificateCustomValidationCallback = (x, y, z, m) => true
            };


            var cert = X509Certificate2.CreateFromPem(_certPem, _keyPem);
            handler.ClientCertificates.Add(cert);

            var client = new HttpClient(handler);
            return client;
        }

        public HttpsClient Builder()
        {
            return new HttpsClient();
        }
    }
}
