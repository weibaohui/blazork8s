namespace Entity.Crd.Gateway;

/// <summary>
/// wget https://raw.githubusercontent.com/kubernetes/kubernetes/master/api/openapi-spec/swagger.json
/// 提取 definitions 部分
/// </summary>
public class GatewaySwaggerDefinition
{
    public static string Definition => """
                                       {
                                           "definitions": {
                                               "io.k8s.apimachinery.pkg.apis.meta.v1.APIGroup": {
                                                     "description": "APIGroup contains the name, the supported versions, and the preferred version of a group.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "versions"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "name is the name of the group.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "preferredVersion": {
                                                             "description": "preferredVersion is the version preferred by the API server, which probably is the storage version.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.GroupVersionForDiscovery"
                                                         },
                                                         "serverAddressByClientCIDRs": {
                                                             "description": "a map of client CIDR to server address that is serving this group. This is to help clients reach servers in the most network-efficient way possible. Clients can use the appropriate server address as per the CIDR that they match. In case of multiple matches, clients should use the longest matching CIDR. The server returns only those CIDRs that it thinks that the client can match. For example: the master will return an internal IP CIDR only, if the client reaches the server using an internal IP. Server looks at X-Forwarded-For header or X-Real-Ip header or request.RemoteAddr (in that order) to get the client IP.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ServerAddressByClientCIDR"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "versions": {
                                                             "description": "versions are the versions supported in this group.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.GroupVersionForDiscovery"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.APIGroupList": {
                                                     "description": "APIGroupList is a list of APIGroup, to allow clients to discover the API at /apis.",
                                                     "type": "object",
                                                     "required": [
                                                         "groups"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "groups": {
                                                             "description": "groups is a list of APIGroup.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.APIGroup"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.APIResource": {
                                                     "description": "APIResource specifies the name of a resource and whether it is namespaced.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "singularName",
                                                         "namespaced",
                                                         "kind",
                                                         "verbs"
                                                     ],
                                                     "properties": {
                                                         "categories": {
                                                             "description": "categories is a list of the grouped resources this resource belongs to (e.g. 'all')",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "group": {
                                                             "description": "group is the preferred group of the resource.  Empty implies the group of the containing resource list. For subresources, this may have a different value, for example: Scale\".",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "kind is the kind for the resource (e.g. 'Foo' is the kind for a resource 'foo')",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "name is the plural name of the resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespaced": {
                                                             "description": "namespaced indicates if a resource is namespaced or not.",
                                                             "type": "boolean",
                                                             "default": false
                                                         },
                                                         "shortNames": {
                                                             "description": "shortNames is a list of suggested short names of the resource.",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "singularName": {
                                                             "description": "singularName is the singular name of the resource.  This allows clients to handle plural and singular opaquely. The singularName is more correct for reporting status on a single item and both singular and plural are allowed from the kubectl CLI interface.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "storageVersionHash": {
                                                             "description": "The hash value of the storage version, the version this resource is converted to when written to the data store. Value must be treated as opaque by clients. Only equality comparison on the value is valid. This is an alpha feature and may change or be removed in the future. The field is populated by the apiserver only if the StorageVersionHash feature gate is enabled. This field will remain optional even if it graduates.",
                                                             "type": "string"
                                                         },
                                                         "verbs": {
                                                             "description": "verbs is a list of supported kube verbs (this includes get, list, watch, create, update, patch, delete, deletecollection, and proxy)",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "version": {
                                                             "description": "version is the preferred version of the resource.  Empty implies the version of the containing resource list For subresources, this may have a different value, for example: v1 (while inside a v1beta1 version of the core resource's group)\".",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.APIResourceList": {
                                                     "description": "APIResourceList is a list of APIResource, it is used to expose the name of the resources supported in a specific group and version, and if the resource is namespaced.",
                                                     "type": "object",
                                                     "required": [
                                                         "groupVersion",
                                                         "resources"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "groupVersion": {
                                                             "description": "groupVersion is the group and version this APIResourceList is for.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "resources": {
                                                             "description": "resources contains the name of the resources and if they are namespaced.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.APIResource"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.APIVersions": {
                                                     "description": "APIVersions lists the versions that are available, to allow clients to discover the API at /api, which is the root path of the legacy v1 API.",
                                                     "type": "object",
                                                     "required": [
                                                         "versions",
                                                         "serverAddressByClientCIDRs"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "serverAddressByClientCIDRs": {
                                                             "description": "a map of client CIDR to server address that is serving this group. This is to help clients reach servers in the most network-efficient way possible. Clients can use the appropriate server address as per the CIDR that they match. In case of multiple matches, clients should use the longest matching CIDR. The server returns only those CIDRs that it thinks that the client can match. For example: the master will return an internal IP CIDR only, if the client reaches the server using an internal IP. Server looks at X-Forwarded-For header or X-Real-Ip header or request.RemoteAddr (in that order) to get the client IP.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ServerAddressByClientCIDR"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "versions": {
                                                             "description": "versions are the api versions that are available.",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.ApplyOptions": {
                                                     "description": "ApplyOptions may be provided when applying an API object. FieldManager is required for apply requests. ApplyOptions is equivalent to PatchOptions. It is provided as a convenience with documentation that speaks specifically to how the options fields relate to apply.",
                                                     "type": "object",
                                                     "required": [
                                                         "force",
                                                         "fieldManager"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "dryRun": {
                                                             "description": "When present, indicates that modifications should not be persisted. An invalid or unrecognized dryRun directive will result in an error response and no further processing of the request. Valid values are: - All: all dry run stages will be processed",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "fieldManager": {
                                                             "description": "fieldManager is a name associated with the actor or entity that is making these changes. The value must be less than or 128 characters long, and only contain printable characters, as defined by https://golang.org/pkg/unicode/#IsPrint. This field is required.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "force": {
                                                             "description": "Force is going to \"force\" Apply requests. It means user will re-acquire conflicting fields owned by other people.",
                                                             "type": "boolean",
                                                             "default": false
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Condition": {
                                                     "description": "Condition contains details for one aspect of the current state of this API Resource.",
                                                     "type": "object",
                                                     "required": [
                                                         "type",
                                                         "status",
                                                         "lastTransitionTime",
                                                         "reason",
                                                         "message"
                                                     ],
                                                     "properties": {
                                                         "lastTransitionTime": {
                                                             "description": "lastTransitionTime is the last time the condition transitioned from one status to another. This should be when the underlying condition changed.  If that is not known, then using the time when the API field changed is acceptable.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Time"
                                                         },
                                                         "message": {
                                                             "description": "message is a human readable message indicating details about the transition. This may be an empty string.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "observedGeneration": {
                                                             "description": "observedGeneration represents the .metadata.generation that the condition was set based upon. For instance, if .metadata.generation is currently 12, but the .status.conditions[x].observedGeneration is 9, the condition is out of date with respect to the current state of the instance.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "reason": {
                                                             "description": "reason contains a programmatic identifier indicating the reason for the condition's last transition. Producers of specific condition types may define expected values and meanings for this field, and whether the values are considered a guaranteed API. The value should be a CamelCase string. This field may not be empty.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "status": {
                                                             "description": "status of the condition, one of True, False, Unknown.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "type": {
                                                             "description": "type of condition in CamelCase or in foo.example.com/CamelCase.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.CreateOptions": {
                                                     "description": "CreateOptions may be provided when creating an API object.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "dryRun": {
                                                             "description": "When present, indicates that modifications should not be persisted. An invalid or unrecognized dryRun directive will result in an error response and no further processing of the request. Valid values are: - All: all dry run stages will be processed",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "fieldManager": {
                                                             "description": "fieldManager is a name associated with the actor or entity that is making these changes. The value must be less than or 128 characters long, and only contain printable characters, as defined by https://golang.org/pkg/unicode/#IsPrint.",
                                                             "type": "string"
                                                         },
                                                         "fieldValidation": {
                                                             "description": "fieldValidation instructs the server on how to handle objects in the request (POST/PUT/PATCH) containing unknown or duplicate fields. Valid values are: - Ignore: This will ignore any unknown fields that are silently dropped from the object, and will ignore all but the last duplicate field that the decoder encounters. This is the default behavior prior to v1.23. - Warn: This will send a warning via the standard warning response header for each unknown field that is dropped from the object, and for each duplicate field that is encountered. The request will still succeed if there are no other errors, and will only persist the last of any duplicate fields. This is the default in v1.23+ - Strict: This will fail the request with a BadRequest error if any unknown fields would be dropped from the object, or if any duplicate fields are present. The error returned from the server will contain all unknown and duplicate fields encountered.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.DeleteOptions": {
                                                     "description": "DeleteOptions may be provided when deleting an API object.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "dryRun": {
                                                             "description": "When present, indicates that modifications should not be persisted. An invalid or unrecognized dryRun directive will result in an error response and no further processing of the request. Valid values are: - All: all dry run stages will be processed",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "gracePeriodSeconds": {
                                                             "description": "The duration in seconds before the object should be deleted. Value must be non-negative integer. The value zero indicates delete immediately. If this value is nil, the default grace period for the specified type will be used. Defaults to a per object value if not specified. zero means delete immediately.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "orphanDependents": {
                                                             "description": "Deprecated: please use the PropagationPolicy, this field will be deprecated in 1.7. Should the dependent objects be orphaned. If true/false, the \"orphan\" finalizer will be added to/removed from the object's finalizers list. Either this field or PropagationPolicy may be set, but not both.",
                                                             "type": "boolean"
                                                         },
                                                         "preconditions": {
                                                             "description": "Must be fulfilled before a deletion is carried out. If not possible, a 409 Conflict status will be returned.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Preconditions"
                                                         },
                                                         "propagationPolicy": {
                                                             "description": "Whether and how garbage collection will be performed. Either this field or OrphanDependents may be set, but not both. The default policy is decided by the existing finalizer set in the metadata.finalizers and the resource-specific default policy. Acceptable values are: 'Orphan' - orphan the dependents; 'Background' - allow the garbage collector to delete the dependents in the background; 'Foreground' - a cascading policy that deletes all dependents in the foreground.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Duration": {
                                                     "description": "Duration is a wrapper around time.Duration which supports correct marshaling to YAML and JSON. In particular, it marshals into strings, which can be used as map keys in json.",
                                                     "type": "string"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.FieldsV1": {
                                                     "description": "FieldsV1 stores a set of fields in a data structure like a Trie, in JSON format.\n\nEach key is either a '.' representing the field itself, and will always map to an empty set, or a string representing a sub-field or item. The string will follow one of these four formats: 'f:\u003cname\u003e', where \u003cname\u003e is the name of a field in a struct, or key in a map 'v:\u003cvalue\u003e', where \u003cvalue\u003e is the exact json formatted value of a list item 'i:\u003cindex\u003e', where \u003cindex\u003e is position of a item in a list 'k:\u003ckeys\u003e', where \u003ckeys\u003e is a map of  a list item's key fields to their unique values If a key maps to an empty Fields value, the field that key represents is part of the set.\n\nThe exact format is defined in sigs.k8s.io/structured-merge-diff",
                                                     "type": "object"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GetOptions": {
                                                     "description": "GetOptions is the standard query options to the standard REST get call.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "resourceVersion": {
                                                             "description": "resourceVersion sets a constraint on what resource versions a request may be served from. See https://kubernetes.io/docs/reference/using-api/api-concepts/#resource-versions for details.\n\nDefaults to unset",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GroupKind": {
                                                     "description": "GroupKind specifies a Group and a Kind, but does not force a version.  This is useful for identifying concepts during lookup stages without having partially valid types",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GroupResource": {
                                                     "description": "GroupResource specifies a Group and a Resource, but does not force a version.  This is useful for identifying concepts during lookup stages without having partially valid types",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "resource"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "resource": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GroupVersion": {
                                                     "description": "GroupVersion contains the \"group\" and the \"version\", which uniquely identifies the API.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "version"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "version": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GroupVersionForDiscovery": {
                                                     "description": "GroupVersion contains the \"group/version\" and \"version\" string of a version. It is made a struct to keep extensibility.",
                                                     "type": "object",
                                                     "required": [
                                                         "groupVersion",
                                                         "version"
                                                     ],
                                                     "properties": {
                                                         "groupVersion": {
                                                             "description": "groupVersion specifies the API group and version in the form \"group/version\"",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "version": {
                                                             "description": "version specifies the version in the form of \"version\". This is to save the clients the trouble of splitting the GroupVersion.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GroupVersionKind": {
                                                     "description": "GroupVersionKind unambiguously identifies a kind.  It doesn't anonymously include GroupVersion to avoid automatic coercion.  It doesn't use a GroupVersion to avoid custom marshalling",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "version",
                                                         "kind"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "version": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.GroupVersionResource": {
                                                     "description": "GroupVersionResource unambiguously identifies a resource.  It doesn't anonymously include GroupVersion to avoid automatic coercion.  It doesn't use a GroupVersion to avoid custom marshalling",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "version",
                                                         "resource"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "resource": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "version": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.InternalEvent": {
                                                     "description": "InternalEvent makes watch.Event versioned",
                                                     "type": "object",
                                                     "required": [
                                                         "Type",
                                                         "Object"
                                                     ],
                                                     "properties": {
                                                         "Object": {
                                                             "description": "Object is:\n * If Type is Added or Modified: the new state of the object.\n * If Type is Deleted: the state of the object immediately before deletion.\n * If Type is Bookmark: the object (instance of a type being watched) where\n   only ResourceVersion field is set. On successful restart of watch from a\n   bookmark resourceVersion, client is guaranteed to not get repeat event\n   nor miss any events.\n * If Type is Error: *api.Status is recommended; other types may make sense\n   depending on context.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.runtime.Object"
                                                         },
                                                         "Type": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.LabelSelector": {
                                                     "description": "A label selector is a label query over a set of resources. The result of matchLabels and matchExpressions are ANDed. An empty label selector matches all objects. A null label selector matches no objects.",
                                                     "type": "object",
                                                     "properties": {
                                                         "matchExpressions": {
                                                             "description": "matchExpressions is a list of label selector requirements. The requirements are ANDed.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.LabelSelectorRequirement"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "matchLabels": {
                                                             "description": "matchLabels is a map of {key,value} pairs. A single {key,value} in the matchLabels map is equivalent to an element of matchExpressions, whose key field is \"key\", the operator is \"In\", and the values array contains only \"value\". The requirements are ANDed.",
                                                             "type": "object",
                                                             "additionalProperties": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         }
                                                     },
                                                     "x-kubernetes-map-type": "atomic"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.LabelSelectorRequirement": {
                                                     "description": "A label selector requirement is a selector that contains values, a key, and an operator that relates the key and values.",
                                                     "type": "object",
                                                     "required": [
                                                         "key",
                                                         "operator"
                                                     ],
                                                     "properties": {
                                                         "key": {
                                                             "description": "key is the label key that the selector applies to.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "operator": {
                                                             "description": "operator represents a key's relationship to a set of values. Valid operators are In, NotIn, Exists and DoesNotExist.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "values": {
                                                             "description": "values is an array of string values. If the operator is In or NotIn, the values array must be non-empty. If the operator is Exists or DoesNotExist, the values array must be empty. This array is replaced during a strategic merge patch.",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.List": {
                                                     "description": "List holds a list of objects, which may not be known by the server.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "description": "List of objects",
                                                             "type": "array",
                                                             "items": {
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.runtime.RawExtension"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "description": "Standard list metadata. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta": {
                                                     "description": "ListMeta describes metadata that synthetic resources must have, including lists and various status objects. A resource may have only one of {ObjectMeta, ListMeta}.",
                                                     "type": "object",
                                                     "properties": {
                                                         "continue": {
                                                             "description": "continue may be set if the user set a limit on the number of items returned, and indicates that the server has more data available. The value is opaque and may be used to issue another request to the endpoint that served this list to retrieve the next set of available objects. Continuing a consistent list may not be possible if the server configuration has changed or more than a few minutes have passed. The resourceVersion field returned when using this continue value will be identical to the value in the first response, unless you have received this token from an error message.",
                                                             "type": "string"
                                                         },
                                                         "remainingItemCount": {
                                                             "description": "remainingItemCount is the number of subsequent items in the list which are not included in this list response. If the list request contained label or field selectors, then the number of remaining items is unknown and the field will be left unset and omitted during serialization. If the list is complete (either because it is not chunking or because this is the last chunk), then there are no more remaining items and this field will be left unset and omitted during serialization. Servers older than v1.15 do not set this field. The intended use of the remainingItemCount is *estimating* the size of a collection. Clients should not rely on the remainingItemCount to be set or to be exact.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "resourceVersion": {
                                                             "description": "String that identifies the server's internal version of this object that can be used by clients to determine when objects have changed. Value must be treated as opaque by clients and passed unmodified back to the server. Populated by the system. Read-only. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#concurrency-control-and-consistency",
                                                             "type": "string"
                                                         },
                                                         "selfLink": {
                                                             "description": "Deprecated: selfLink is a legacy read-only field that is no longer populated by the system.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.ListOptions": {
                                                     "description": "ListOptions is the query options to a standard REST list call.",
                                                     "type": "object",
                                                     "properties": {
                                                         "allowWatchBookmarks": {
                                                             "description": "allowWatchBookmarks requests watch events with type \"BOOKMARK\". Servers that do not implement bookmarks may ignore this flag and bookmarks are sent at the server's discretion. Clients should not assume bookmarks are returned at any specific interval, nor may they assume the server will send any BOOKMARK event during a session. If this is not a watch, this field is ignored.",
                                                             "type": "boolean"
                                                         },
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "continue": {
                                                             "description": "The continue option should be set when retrieving more results from the server. Since this value is server defined, clients may only use the continue value from a previous query result with identical query parameters (except for the value of continue) and the server may reject a continue value it does not recognize. If the specified continue value is no longer valid whether due to expiration (generally five to fifteen minutes) or a configuration change on the server, the server will respond with a 410 ResourceExpired error together with a continue token. If the client needs a consistent list, it must restart their list without the continue field. Otherwise, the client may send another list request with the token received with the 410 error, the server will respond with a list starting from the next key, but from the latest snapshot, which is inconsistent from the previous list results - objects that are created, modified, or deleted after the first list request will be included in the response, as long as their keys are after the \"next key\".\n\nThis field is not supported when watch is true. Clients may start a watch from the last resourceVersion value returned by the server and not miss any modifications.",
                                                             "type": "string"
                                                         },
                                                         "fieldSelector": {
                                                             "description": "A selector to restrict the list of returned objects by their fields. Defaults to everything.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "labelSelector": {
                                                             "description": "A selector to restrict the list of returned objects by their labels. Defaults to everything.",
                                                             "type": "string"
                                                         },
                                                         "limit": {
                                                             "description": "limit is a maximum number of responses to return for a list call. If more items exist, the server will set the `continue` field on the list metadata to a value that can be used with the same initial query to retrieve the next set of results. Setting a limit may return fewer than the requested amount of items (up to zero items) in the event all requested objects are filtered out and clients should only use the presence of the continue field to determine whether more results are available. Servers may choose not to support the limit argument and will return all of the available results. If limit is specified and the continue field is empty, clients may assume that no more results are available. This field is not supported if watch is true.\n\nThe server guarantees that the objects returned when using continue will be identical to issuing a single list call without a limit - that is, no objects created, modified, or deleted after the first request is issued will be included in any subsequent continued requests. This is sometimes referred to as a consistent snapshot, and ensures that a client that is using limit to receive smaller chunks of a very large result can ensure they see all possible objects. If objects are updated during a chunked list the version of the object that was present at the time the first list result was calculated is returned.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "resourceVersion": {
                                                             "description": "resourceVersion sets a constraint on what resource versions a request may be served from. See https://kubernetes.io/docs/reference/using-api/api-concepts/#resource-versions for details.\n\nDefaults to unset",
                                                             "type": "string"
                                                         },
                                                         "resourceVersionMatch": {
                                                             "description": "resourceVersionMatch determines how resourceVersion is applied to list calls. It is highly recommended that resourceVersionMatch be set for list calls where resourceVersion is set See https://kubernetes.io/docs/reference/using-api/api-concepts/#resource-versions for details.\n\nDefaults to unset",
                                                             "type": "string"
                                                         },
                                                         "sendInitialEvents": {
                                                             "description": "`sendInitialEvents=true` may be set together with `watch=true`. In that case, the watch stream will begin with synthetic events to produce the current state of objects in the collection. Once all such events have been sent, a synthetic \"Bookmark\" event  will be sent. The bookmark will report the ResourceVersion (RV) corresponding to the set of objects, and be marked with `\"k8s.io/initial-events-end\": \"true\"` annotation. Afterwards, the watch stream will proceed as usual, sending watch events corresponding to changes (subsequent to the RV) to objects watched.\n\nWhen `sendInitialEvents` option is set, we require `resourceVersionMatch` option to also be set. The semantic of the watch request is as following: - `resourceVersionMatch` = NotOlderThan\n  is interpreted as \"data at least as new as the provided `resourceVersion`\"\n  and the bookmark event is send when the state is synced\n  to a `resourceVersion` at least as fresh as the one provided by the ListOptions.\n  If `resourceVersion` is unset, this is interpreted as \"consistent read\" and the\n  bookmark event is send when the state is synced at least to the moment\n  when request started being processed.\n- `resourceVersionMatch` set to any other value or unset\n  Invalid error is returned.\n\nDefaults to true if `resourceVersion=\"\"` or `resourceVersion=\"0\"` (for backward compatibility reasons) and to false otherwise.",
                                                             "type": "boolean"
                                                         },
                                                         "timeoutSeconds": {
                                                             "description": "Timeout for the list/watch call. This limits the duration of the call, regardless of any activity or inactivity.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "watch": {
                                                             "description": "Watch for changes to the described resources and return them as a stream of add, update, and remove notifications. Specify resourceVersion.",
                                                             "type": "boolean"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.ManagedFieldsEntry": {
                                                     "description": "ManagedFieldsEntry is a workflow-id, a FieldSet and the group version of the resource that the fieldset applies to.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the version of this resource that this field set applies to. The format is \"group/version\" just like the top-level APIVersion field. It is necessary to track the version of a field set because it cannot be automatically converted.",
                                                             "type": "string"
                                                         },
                                                         "fieldsType": {
                                                             "description": "FieldsType is the discriminator for the different fields format and version. There is currently only one possible value: \"FieldsV1\"",
                                                             "type": "string"
                                                         },
                                                         "fieldsV1": {
                                                             "description": "FieldsV1 holds the first JSON version format as described in the \"FieldsV1\" type.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.FieldsV1"
                                                         },
                                                         "manager": {
                                                             "description": "Manager is an identifier of the workflow managing these fields.",
                                                             "type": "string"
                                                         },
                                                         "operation": {
                                                             "description": "Operation is the type of operation which lead to this ManagedFieldsEntry being created. The only valid values for this field are 'Apply' and 'Update'.",
                                                             "type": "string"
                                                         },
                                                         "subresource": {
                                                             "description": "Subresource is the name of the subresource used to update that object, or empty string if the object was updated through the main resource. The value of this field is used to distinguish between managers, even if they share the same name. For example, a status update will be distinct from a regular update using the same manager name. Note that the APIVersion field is not related to the Subresource field and it always corresponds to the version of the main resource.",
                                                             "type": "string"
                                                         },
                                                         "time": {
                                                             "description": "Time is the timestamp of when the ManagedFields entry was added. The timestamp will also be updated if a field is added, the manager changes any of the owned fields value or removes a field. The timestamp does not update when a field is removed from the entry because another manager took it over.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Time"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.MicroTime": {
                                                     "description": "MicroTime is version of Time with microsecond level precision.",
                                                     "type": "string",
                                                     "format": "date-time"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta": {
                                                     "description": "ObjectMeta is metadata that all persisted resources must have, which includes all objects users must create.",
                                                     "type": "object",
                                                     "properties": {
                                                         "annotations": {
                                                             "description": "Annotations is an unstructured key value map stored with a resource that may be set by external tools to store and retrieve arbitrary metadata. They are not queryable and should be preserved when modifying objects. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/annotations",
                                                             "type": "object",
                                                             "additionalProperties": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "creationTimestamp": {
                                                             "description": "CreationTimestamp is a timestamp representing the server time when this object was created. It is not guaranteed to be set in happens-before order across separate operations. Clients may not set this value. It is represented in RFC3339 form and is in UTC.\n\nPopulated by the system. Read-only. Null for lists. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#metadata",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Time"
                                                         },
                                                         "deletionGracePeriodSeconds": {
                                                             "description": "Number of seconds allowed for this object to gracefully terminate before it will be removed from the system. Only set when deletionTimestamp is also set. May only be shortened. Read-only.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "deletionTimestamp": {
                                                             "description": "DeletionTimestamp is RFC 3339 date and time at which this resource will be deleted. This field is set by the server when a graceful deletion is requested by the user, and is not directly settable by a client. The resource is expected to be deleted (no longer visible from resource lists, and not reachable by name) after the time in this field, once the finalizers list is empty. As long as the finalizers list contains items, deletion is blocked. Once the deletionTimestamp is set, this value may not be unset or be set further into the future, although it may be shortened or the resource may be deleted prior to this time. For example, a user may request that a pod is deleted in 30 seconds. The Kubelet will react by sending a graceful termination signal to the containers in the pod. After that 30 seconds, the Kubelet will send a hard termination signal (SIGKILL) to the container and after cleanup, remove the pod from the API. In the presence of network partitions, this object may still exist after this timestamp, until an administrator or automated process can determine the resource is fully terminated. If not set, graceful deletion of the object has not been requested.\n\nPopulated by the system when a graceful deletion is requested. Read-only. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#metadata",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Time"
                                                         },
                                                         "finalizers": {
                                                             "description": "Must be empty before the object is deleted from the registry. Each entry is an identifier for the responsible component that will remove the entry from the list. If the deletionTimestamp of the object is non-nil, entries in this list can only be removed. Finalizers may be processed and removed in any order.  Order is NOT enforced because it introduces significant risk of stuck finalizers. finalizers is a shared field, any actor with permission can reorder it. If the finalizer list is processed in order, then this can lead to a situation in which the component responsible for the first finalizer in the list is waiting for a signal (field value, external system, or other) produced by a component responsible for a finalizer later in the list, resulting in a deadlock. Without enforced ordering finalizers are free to order amongst themselves and are not vulnerable to ordering changes in the list.",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "set",
                                                             "x-kubernetes-patch-strategy": "merge"
                                                         },
                                                         "generateName": {
                                                             "description": "GenerateName is an optional prefix, used by the server, to generate a unique name ONLY IF the Name field has not been provided. If this field is used, the name returned to the client will be different than the name passed. This value will also be combined with a unique suffix. The provided value has the same validation rules as the Name field, and may be truncated by the length of the suffix required to make the value unique on the server.\n\nIf this field is specified and the generated name exists, the server will return a 409.\n\nApplied only if Name is not specified. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#idempotency",
                                                             "type": "string"
                                                         },
                                                         "generation": {
                                                             "description": "A sequence number representing a specific generation of the desired state. Populated by the system. Read-only.",
                                                             "type": "integer",
                                                             "format": "int64"
                                                         },
                                                         "labels": {
                                                             "description": "Map of string keys and values that can be used to organize and categorize (scope and select) objects. May match selectors of replication controllers and services. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/labels",
                                                             "type": "object",
                                                             "additionalProperties": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "managedFields": {
                                                             "description": "ManagedFields maps workflow-id and version to the set of fields that are managed by that workflow. This is mostly for internal housekeeping, and users typically shouldn't need to set or understand this field. A workflow can be the user's name, a controller's name, or the name of a specific apply path like \"ci-cd\". The set of fields is always in the version that the workflow used when modifying the object.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ManagedFieldsEntry"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "name": {
                                                             "description": "Name must be unique within a namespace. Is required when creating resources, although some resources may allow a client to request the generation of an appropriate name automatically. Name is primarily intended for creation idempotence and configuration definition. Cannot be updated. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#names",
                                                             "type": "string"
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace defines the space within which each name must be unique. An empty namespace is equivalent to the \"default\" namespace, but \"default\" is the canonical representation. Not all objects are required to be scoped to a namespace - the value of this field for those objects will be empty.\n\nMust be a DNS_LABEL. Cannot be updated. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces",
                                                             "type": "string"
                                                         },
                                                         "ownerReferences": {
                                                             "description": "List of objects depended by this object. If ALL objects in the list have been deleted, this object will be garbage collected. If this object is managed by a controller, then an entry in this list will point to this controller, with the controller field set to true. There cannot be more than one managing controller.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.OwnerReference"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "uid"
                                                             ],
                                                             "x-kubernetes-list-type": "map",
                                                             "x-kubernetes-patch-merge-key": "uid",
                                                             "x-kubernetes-patch-strategy": "merge"
                                                         },
                                                         "resourceVersion": {
                                                             "description": "An opaque value that represents the internal version of this object that can be used by clients to determine when objects have changed. May be used for optimistic concurrency, change detection, and the watch operation on a resource or set of resources. Clients must treat these values as opaque and passed unmodified back to the server. They may only be valid for a particular resource or set of resources.\n\nPopulated by the system. Read-only. Value must be treated as opaque by clients and . More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#concurrency-control-and-consistency",
                                                             "type": "string"
                                                         },
                                                         "selfLink": {
                                                             "description": "Deprecated: selfLink is a legacy read-only field that is no longer populated by the system.",
                                                             "type": "string"
                                                         },
                                                         "uid": {
                                                             "description": "UID is the unique in time and space value for this object. It is typically generated by the server on successful creation of a resource and is not allowed to change on PUT operations.\n\nPopulated by the system. Read-only. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#uids",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.OwnerReference": {
                                                     "description": "OwnerReference contains enough information to let you identify an owning object. An owning object must be in the same namespace as the dependent, or be cluster-scoped, so there is no namespace field.",
                                                     "type": "object",
                                                     "required": [
                                                         "apiVersion",
                                                         "kind",
                                                         "name",
                                                         "uid"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "API version of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "blockOwnerDeletion": {
                                                             "description": "If true, AND if the owner has the \"foregroundDeletion\" finalizer, then the owner cannot be deleted from the key-value store until this reference is removed. See https://kubernetes.io/docs/concepts/architecture/garbage-collection/#foreground-deletion for how the garbage collector interacts with this field and enforces the foreground deletion. Defaults to false. To set this field, a user needs \"delete\" permission of the owner, otherwise 422 (Unprocessable Entity) will be returned.",
                                                             "type": "boolean"
                                                         },
                                                         "controller": {
                                                             "description": "If true, this reference points to the managing controller.",
                                                             "type": "boolean"
                                                         },
                                                         "kind": {
                                                             "description": "Kind of the referent. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#names",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "uid": {
                                                             "description": "UID of the referent. More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#uids",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     },
                                                     "x-kubernetes-map-type": "atomic"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.PartialObjectMetadata": {
                                                     "description": "PartialObjectMetadata is a generic representation of any object with ObjectMeta. It allows clients to get access to a particular ObjectMeta schema without knowing the details of the version.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "description": "Standard object's metadata. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#metadata",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.PartialObjectMetadataList": {
                                                     "description": "PartialObjectMetadataList contains a list of objects containing only their metadata",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "description": "items contains each of the included items.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.PartialObjectMetadata"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "description": "Standard list metadata. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Patch": {
                                                     "description": "Patch is provided to give a concrete name and type to the Kubernetes PATCH request body.",
                                                     "type": "object"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.PatchOptions": {
                                                     "description": "PatchOptions may be provided when patching an API object. PatchOptions is meant to be a superset of UpdateOptions.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "dryRun": {
                                                             "description": "When present, indicates that modifications should not be persisted. An invalid or unrecognized dryRun directive will result in an error response and no further processing of the request. Valid values are: - All: all dry run stages will be processed",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "fieldManager": {
                                                             "description": "fieldManager is a name associated with the actor or entity that is making these changes. The value must be less than or 128 characters long, and only contain printable characters, as defined by https://golang.org/pkg/unicode/#IsPrint. This field is required for apply requests (application/apply-patch) but optional for non-apply patch types (JsonPatch, MergePatch, StrategicMergePatch).",
                                                             "type": "string"
                                                         },
                                                         "fieldValidation": {
                                                             "description": "fieldValidation instructs the server on how to handle objects in the request (POST/PUT/PATCH) containing unknown or duplicate fields. Valid values are: - Ignore: This will ignore any unknown fields that are silently dropped from the object, and will ignore all but the last duplicate field that the decoder encounters. This is the default behavior prior to v1.23. - Warn: This will send a warning via the standard warning response header for each unknown field that is dropped from the object, and for each duplicate field that is encountered. The request will still succeed if there are no other errors, and will only persist the last of any duplicate fields. This is the default in v1.23+ - Strict: This will fail the request with a BadRequest error if any unknown fields would be dropped from the object, or if any duplicate fields are present. The error returned from the server will contain all unknown and duplicate fields encountered.",
                                                             "type": "string"
                                                         },
                                                         "force": {
                                                             "description": "Force is going to \"force\" Apply requests. It means user will re-acquire conflicting fields owned by other people. Force flag must be unset for non-apply patch requests.",
                                                             "type": "boolean"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Preconditions": {
                                                     "description": "Preconditions must be fulfilled before an operation (update, delete, etc.) is carried out.",
                                                     "type": "object",
                                                     "properties": {
                                                         "resourceVersion": {
                                                             "description": "Specifies the target ResourceVersion",
                                                             "type": "string"
                                                         },
                                                         "uid": {
                                                             "description": "Specifies the target UID.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.RootPaths": {
                                                     "description": "RootPaths lists the paths available at root. For example: \"/healthz\", \"/apis\".",
                                                     "type": "object",
                                                     "required": [
                                                         "paths"
                                                     ],
                                                     "properties": {
                                                         "paths": {
                                                             "description": "paths are the paths available at root.",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.ServerAddressByClientCIDR": {
                                                     "description": "ServerAddressByClientCIDR helps the client to determine the server address that they should use, depending on the clientCIDR that they match.",
                                                     "type": "object",
                                                     "required": [
                                                         "clientCIDR",
                                                         "serverAddress"
                                                     ],
                                                     "properties": {
                                                         "clientCIDR": {
                                                             "description": "The CIDR with which clients can match their IP to figure out the server address that they should use.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "serverAddress": {
                                                             "description": "Address of this server, suitable for a client that matches the above CIDR. This can be a hostname, hostname:port, IP or IP:port.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Status": {
                                                     "description": "Status is a return value for calls that don't return other objects.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "code": {
                                                             "description": "Suggested HTTP return code for this status, 0 if not set.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "details": {
                                                             "description": "Extended data associated with the reason.  Each reason may define its own extended details. This field is optional and the data returned is not guaranteed to conform to any schema except that defined by the reason type.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.StatusDetails",
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "message": {
                                                             "description": "A human-readable description of the status of this operation.",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "description": "Standard list metadata. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         },
                                                         "reason": {
                                                             "description": "A machine-readable description of why this operation is in the \"Failure\" status. If this value is empty there is no information available. A Reason clarifies an HTTP status code but does not override it.",
                                                             "type": "string"
                                                         },
                                                         "status": {
                                                             "description": "Status of the operation. One of: \"Success\" or \"Failure\". More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#spec-and-status",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.StatusCause": {
                                                     "description": "StatusCause provides more information about an api.Status failure, including cases when multiple errors are encountered.",
                                                     "type": "object",
                                                     "properties": {
                                                         "field": {
                                                             "description": "The field of the resource that has caused this error, as named by its JSON serialization. May include dot and postfix notation for nested attributes. Arrays are zero-indexed.  Fields may appear more than once in an array of causes due to fields having multiple errors. Optional.\n\nExamples:\n  \"name\" - the field \"name\" on the current resource\n  \"items[0].name\" - the field \"name\" on the first array entry in \"items\"",
                                                             "type": "string"
                                                         },
                                                         "message": {
                                                             "description": "A human-readable description of the cause of the error.  This field may be presented as-is to a reader.",
                                                             "type": "string"
                                                         },
                                                         "reason": {
                                                             "description": "A machine-readable description of the cause of the error. If this value is empty there is no information available.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.StatusDetails": {
                                                     "description": "StatusDetails is a set of additional properties that MAY be set by the server to provide additional information about a response. The Reason field of a Status object defines what attributes will be set. Clients must ignore fields that do not match the defined type of each attribute, and should assume that any attribute may be empty, invalid, or under defined.",
                                                     "type": "object",
                                                     "properties": {
                                                         "causes": {
                                                             "description": "The Causes array includes more details associated with the StatusReason failure. Not all StatusReasons may provide detailed causes.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.StatusCause"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "group": {
                                                             "description": "The group attribute of the resource associated with the status StatusReason.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "The kind attribute of the resource associated with the status StatusReason. On some operations may differ from the requested resource Kind. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "The name attribute of the resource associated with the status StatusReason (when there is a single name which can be described).",
                                                             "type": "string"
                                                         },
                                                         "retryAfterSeconds": {
                                                             "description": "If specified, the time in seconds before the operation should be retried. Some errors may indicate the client must take an alternate action - for those errors this field may indicate how long to wait before taking the alternate action.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "uid": {
                                                             "description": "UID of the resource. (when there is a single resource which can be described). More info: https://kubernetes.io/docs/concepts/overview/working-with-objects/names#uids",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Table": {
                                                     "description": "Table is a tabular representation of a set of API resources. The server transforms the object into a set of preferred columns for quickly reviewing the objects.",
                                                     "type": "object",
                                                     "required": [
                                                         "columnDefinitions",
                                                         "rows"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "columnDefinitions": {
                                                             "description": "columnDefinitions describes each column in the returned items array. The number of cells per row will always match the number of column definitions.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.TableColumnDefinition"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "description": "Standard list metadata. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         },
                                                         "rows": {
                                                             "description": "rows is the list of items in the table.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.TableRow"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.TableColumnDefinition": {
                                                     "description": "TableColumnDefinition contains information about a column returned in the Table.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "type",
                                                         "format",
                                                         "description",
                                                         "priority"
                                                     ],
                                                     "properties": {
                                                         "description": {
                                                             "description": "description is a human readable description of this column.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "format": {
                                                             "description": "format is an optional OpenAPI type modifier for this column. A format modifies the type and imposes additional rules, like date or time formatting for a string. The 'name' format is applied to the primary identifier column which has type 'string' to assist in clients identifying column is the resource name. See https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md#data-types for more.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "name is a human readable name for the column.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "priority": {
                                                             "description": "priority is an integer defining the relative importance of this column compared to others. Lower numbers are considered higher priority. Columns that may be omitted in limited space scenarios should be given a higher priority.",
                                                             "type": "integer",
                                                             "format": "int32",
                                                             "default": 0
                                                         },
                                                         "type": {
                                                             "description": "type is an OpenAPI type definition for this column, such as number, integer, string, or array. See https://github.com/OAI/OpenAPI-Specification/blob/master/versions/2.0.md#data-types for more.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.TableOptions": {
                                                     "description": "TableOptions are used when a Table is requested by the caller.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "includeObject": {
                                                             "description": "includeObject decides whether to include each object along with its columnar information. Specifying \"None\" will return no object, specifying \"Object\" will return the full object contents, and specifying \"Metadata\" (the default) will return the object's metadata in the PartialObjectMetadata kind in version v1beta1 of the meta.k8s.io API group.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.TableRow": {
                                                     "description": "TableRow is an individual row in a table.",
                                                     "type": "object",
                                                     "required": [
                                                         "cells"
                                                     ],
                                                     "properties": {
                                                         "cells": {
                                                             "description": "cells will be as wide as the column definitions array and may contain strings, numbers (float64 or int64), booleans, simple maps, lists, or null. See the type field of the column definition for a more detailed description.",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "object"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "conditions": {
                                                             "description": "conditions describe additional status of a row that are relevant for a human user. These conditions apply to the row, not to the object, and will be specific to table output. The only defined condition type is 'Completed', for a row that indicates a resource that has run to completion and can be given less visual priority.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.TableRowCondition"
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "object": {
                                                             "description": "This field contains the requested additional information about each object based on the includeObject policy when requesting the Table. If \"None\", this field is empty, if \"Object\" this will be the default serialization of the object for the current API version, and if \"Metadata\" (the default) will contain the object metadata. Check the returned kind and apiVersion of the object before parsing. The media type of the object will always match the enclosing list - if this as a JSON table, these will be JSON encoded objects.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.runtime.RawExtension"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.TableRowCondition": {
                                                     "description": "TableRowCondition allows a row to be marked with additional information.",
                                                     "type": "object",
                                                     "required": [
                                                         "type",
                                                         "status"
                                                     ],
                                                     "properties": {
                                                         "message": {
                                                             "description": "Human readable message indicating details about last transition.",
                                                             "type": "string"
                                                         },
                                                         "reason": {
                                                             "description": "(brief) machine readable reason for the condition's last transition.",
                                                             "type": "string"
                                                         },
                                                         "status": {
                                                             "description": "Status of the condition, one of True, False, Unknown.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "type": {
                                                             "description": "Type of row condition. The only defined value is 'Completed' indicating that the object this row represents has reached a completed state and may be given less visual priority than other rows. Clients are not required to honor any conditions but should be consistent where possible about handling the conditions.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Time": {
                                                     "description": "Time is a wrapper around time.Time which supports correct marshaling to YAML and JSON.  Wrappers are provided for many of the factory methods that the time package offers.",
                                                     "type": "string",
                                                     "format": "date-time"
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.Timestamp": {
                                                     "description": "Timestamp is a struct that is equivalent to Time, but intended for protobuf marshalling/unmarshalling. It is generated into a serialization that matches Time. Do not use in Go structs.",
                                                     "type": "object",
                                                     "required": [
                                                         "seconds",
                                                         "nanos"
                                                     ],
                                                     "properties": {
                                                         "nanos": {
                                                             "description": "Non-negative fractions of a second at nanosecond resolution. Negative second values with fractions must still have non-negative nanos values that count forward in time. Must be from 0 to 999,999,999 inclusive. This field may be limited in precision depending on context.",
                                                             "type": "integer",
                                                             "format": "int32",
                                                             "default": 0
                                                         },
                                                         "seconds": {
                                                             "description": "Represents seconds of UTC time since Unix epoch 1970-01-01T00:00:00Z. Must be from 0001-01-01T00:00:00Z to 9999-12-31T23:59:59Z inclusive.",
                                                             "type": "integer",
                                                             "format": "int64",
                                                             "default": 0
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.TypeMeta": {
                                                     "description": "TypeMeta describes an individual object in an API response or request with strings representing the type of the object and its API schema version. Structures that are versioned or persisted should inline TypeMeta.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.UpdateOptions": {
                                                     "description": "UpdateOptions may be provided when updating an API object. All fields in UpdateOptions should also be present in PatchOptions.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "dryRun": {
                                                             "description": "When present, indicates that modifications should not be persisted. An invalid or unrecognized dryRun directive will result in an error response and no further processing of the request. Valid values are: - All: all dry run stages will be processed",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "atomic"
                                                         },
                                                         "fieldManager": {
                                                             "description": "fieldManager is a name associated with the actor or entity that is making these changes. The value must be less than or 128 characters long, and only contain printable characters, as defined by https://golang.org/pkg/unicode/#IsPrint.",
                                                             "type": "string"
                                                         },
                                                         "fieldValidation": {
                                                             "description": "fieldValidation instructs the server on how to handle objects in the request (POST/PUT/PATCH) containing unknown or duplicate fields. Valid values are: - Ignore: This will ignore any unknown fields that are silently dropped from the object, and will ignore all but the last duplicate field that the decoder encounters. This is the default behavior prior to v1.23. - Warn: This will send a warning via the standard warning response header for each unknown field that is dropped from the object, and for each duplicate field that is encountered. The request will still succeed if there are no other errors, and will only persist the last of any duplicate fields. This is the default in v1.23+ - Strict: This will fail the request with a BadRequest error if any unknown fields would be dropped from the object, or if any duplicate fields are present. The error returned from the server will contain all unknown and duplicate fields encountered.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.apis.meta.v1.WatchEvent": {
                                                     "description": "Event represents a single event to a watched resource.",
                                                     "type": "object",
                                                     "required": [
                                                         "type",
                                                         "object"
                                                     ],
                                                     "properties": {
                                                         "object": {
                                                             "description": "Object is:\n * If Type is Added or Modified: the new state of the object.\n * If Type is Deleted: the state of the object immediately before deletion.\n * If Type is Error: *Status is recommended; other types may make sense\n   depending on context.",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.runtime.RawExtension"
                                                         },
                                                         "type": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.runtime.RawExtension": {
                                                     "description": "RawExtension is used to hold extensions in external versions.\n\nTo use this, make a field which has RawExtension as its type in your external, versioned struct, and Object in your internal struct. You also need to register your various plugin types.\n\n// Internal package:\n\n\ttype MyAPIObject struct {\n\t\truntime.TypeMeta `json:\",inline\"`\n\t\tMyPlugin runtime.Object `json:\"myPlugin\"`\n\t}\n\n\ttype PluginA struct {\n\t\tAOption string `json:\"aOption\"`\n\t}\n\n// External package:\n\n\ttype MyAPIObject struct {\n\t\truntime.TypeMeta `json:\",inline\"`\n\t\tMyPlugin runtime.RawExtension `json:\"myPlugin\"`\n\t}\n\n\ttype PluginA struct {\n\t\tAOption string `json:\"aOption\"`\n\t}\n\n// On the wire, the JSON will look something like this:\n\n\t{\n\t\t\"kind\":\"MyAPIObject\",\n\t\t\"apiVersion\":\"v1\",\n\t\t\"myPlugin\": {\n\t\t\t\"kind\":\"PluginA\",\n\t\t\t\"aOption\":\"foo\",\n\t\t},\n\t}\n\nSo what happens? Decode first uses json or yaml to unmarshal the serialized data into your external MyAPIObject. That causes the raw JSON to be stored, but not unpacked. The next step is to copy (using pkg/conversion) into the internal struct. The runtime package's DefaultScheme has conversion functions installed which will unpack the JSON stored in RawExtension, turning it into the correct object type, and storing it in the Object. (TODO: In the case where the object is of an unknown type, a runtime.Unknown object will be created and stored.)",
                                                     "type": "object"
                                                 },
                                                 "io.k8s.apimachinery.pkg.runtime.TypeMeta": {
                                                     "description": "TypeMeta is shared by all top level objects. The proper way to use it is to inline it in your type, like this:\n\n\ttype MyAwesomeAPIObject struct {\n\t     runtime.TypeMeta    `json:\",inline\"`\n\t     ... // other fields\n\t}\n\nfunc (obj *MyAwesomeAPIObject) SetGroupVersionKind(gvk *metav1.GroupVersionKind) { metav1.UpdateTypeMeta(obj,gvk) }; GroupVersionKind() *GroupVersionKind\n\nTypeMeta is provided here for convenience. You may use it directly from this package or define your own with the same fields.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.runtime.Unknown": {
                                                     "description": "Unknown allows api objects with unknown types to be passed-through. This can be used to deal with the API objects from a plug-in. Unknown objects still have functioning TypeMeta features-- kind, version, etc. metadata and field mutatation.",
                                                     "type": "object",
                                                     "required": [
                                                         "ContentEncoding",
                                                         "ContentType"
                                                     ],
                                                     "properties": {
                                                         "ContentEncoding": {
                                                             "description": "ContentEncoding is encoding used to encode 'Raw' data. Unspecified means no encoding.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "ContentType": {
                                                             "description": "ContentType  is serialization method used to serialize 'Raw'. Unspecified means ContentTypeJSON.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "apiVersion": {
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.apimachinery.pkg.version.Info": {
                                                     "description": "Info contains versioning information. how we'll want to distribute that information.",
                                                     "type": "object",
                                                     "required": [
                                                         "major",
                                                         "minor",
                                                         "gitVersion",
                                                         "gitCommit",
                                                         "gitTreeState",
                                                         "buildDate",
                                                         "goVersion",
                                                         "compiler",
                                                         "platform"
                                                     ],
                                                     "properties": {
                                                         "buildDate": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "compiler": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "gitCommit": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "gitTreeState": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "gitVersion": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "goVersion": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "major": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "minor": {
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "platform": {
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.AllowedRoutes": {
                                                     "description": "AllowedRoutes defines which Routes may be attached to this Listener.",
                                                     "type": "object",
                                                     "properties": {
                                                         "kinds": {
                                                             "description": "Kinds specifies the groups and kinds of Routes that are allowed to bind to this Gateway Listener. When unspecified or empty, the kinds of Routes selected are determined using the Listener protocol.\n\nA RouteGroupKind MUST correspond to kinds of Routes that are compatible with the application protocol specified in the Listener's Protocol field. If an implementation does not support or recognize this resource type, it MUST set the \"ResolvedRefs\" condition to False for this Listener with the \"InvalidRouteKinds\" reason.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteGroupKind"
                                                             }
                                                         },
                                                         "namespaces": {
                                                             "description": "Namespaces indicates namespaces from which Routes may be attached to this Listener. This is restricted to the namespace of this Gateway by default.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteNamespaces"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.BackendObjectReference": {
                                                     "description": "BackendObjectReference defines how an ObjectReference that is specific to BackendRef. It includes a few additional fields and features than a regular ObjectReference.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nThe API object must be valid in the cluster; the Group and Kind must be registered in the cluster for this reference to be valid.\n\nReferences to objects with invalid Group and Kind are not valid, and must be rejected by the implementation, with appropriate Conditions set on the containing object.",
                                                     "type": "object",
                                                     "required": [
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the Kubernetes resource kind of the referent. For example \"Service\".\n\nDefaults to \"Service\" when not specified.\n\nExternalName services can refer to CNAME DNS records that may live outside of the cluster and as such are difficult to reason about in terms of conformance. They also may not be safe to forward to (see CVE-2021-25740 for more information). Implementations SHOULD NOT support ExternalName Services.\n\nSupport: Core (Services with a type other than ExternalName)\n\nSupport: Implementation-specific (Services with type ExternalName)",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the backend. When unspecified, the local namespace is inferred.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "port": {
                                                             "description": "Port specifies the destination port number to use for this resource. Port is required when the referent is a Kubernetes Service. In this case, the port number is the service port number, not the target port. For other resources, destination port might be derived from the referent resource or this field.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.BackendRef": {
                                                     "description": "BackendRef defines how a Route should forward a request to a Kubernetes resource.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.",
                                                     "type": "object",
                                                     "required": [
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the Kubernetes resource kind of the referent. For example \"Service\".\n\nDefaults to \"Service\" when not specified.\n\nExternalName services can refer to CNAME DNS records that may live outside of the cluster and as such are difficult to reason about in terms of conformance. They also may not be safe to forward to (see CVE-2021-25740 for more information). Implementations SHOULD NOT support ExternalName Services.\n\nSupport: Core (Services with a type other than ExternalName)\n\nSupport: Implementation-specific (Services with type ExternalName)",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the backend. When unspecified, the local namespace is inferred.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "port": {
                                                             "description": "Port specifies the destination port number to use for this resource. Port is required when the referent is a Kubernetes Service. In this case, the port number is the service port number, not the target port. For other resources, destination port might be derived from the referent resource or this field.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "weight": {
                                                             "description": "Weight specifies the proportion of requests forwarded to the referenced backend. This is computed as weight/(sum of all weights in this BackendRefs list). For non-zero values, there may be some epsilon from the exact proportion defined here depending on the precision an implementation supports. Weight is not a percentage and the sum of weights does not need to equal 100.\n\nIf only one backend is specified and it has a weight greater than 0, 100% of the traffic is forwarded to that backend. If weight is set to 0, no traffic should be forwarded for this entry. If unspecified, weight defaults to 1.\n\nSupport for this field varies based on the context where used.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.CommonRouteSpec": {
                                                     "description": "CommonRouteSpec defines the common attributes that all Routes MUST include within their spec.",
                                                     "type": "object",
                                                     "properties": {
                                                         "parentRefs": {
                                                             "description": "ParentRefs references the resources (usually Gateways) that a Route wants to be attached to. Note that the referenced parent resource needs to allow this for the attachment to be complete. For Gateways, that means the Gateway needs to allow attachment from Routes of this kind and namespace. For Services, that means the Service must either be in the same namespace for a \"producer\" route, or the mesh implementation must support and allow \"consumer\" routes for the referenced Service. ReferenceGrant is not applicable for governing ParentRefs to Services - it is not possible to create a \"producer\" route for a Service in a different namespace from the Route.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nThis API may be extended in the future to support additional kinds of parent resources.\n\nParentRefs must be _distinct_. This means either that:\n\n* They select different objects.  If this is the case, then parentRef\n  entries are distinct. In terms of fields, this means that the\n  multi-part key defined by `group`, `kind`, `namespace`, and `name` must\n  be unique across all parentRef entries in the Route.\n* They do not select different objects, but for each optional field used,\n  each ParentRef that selects the same object must set the same set of\n  optional fields to different values. If one ParentRef sets a\n  combination of optional fields, all must set the same combination.\n\nSome examples:\n\n* If one ParentRef sets `sectionName`, all ParentRefs referencing the\n  same object must also set `sectionName`.\n* If one ParentRef sets `port`, all ParentRefs referencing the same\n  object must also set `port`.\n* If one ParentRef sets `sectionName` and `port`, all ParentRefs\n  referencing the same object must also set `sectionName` and `port`.\n\nIt is possible to separately reference multiple distinct objects that may be collapsed by an implementation. For example, some implementations may choose to merge compatible Gateway Listeners together. If that is the case, the list of routes attached to those resources should also be merged.\n\nNote that for ParentRefs that cross namespace boundaries, there are specific rules. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example, Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable other kinds of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\n\u003cgateway:standard:validation:XValidation:message=\"sectionName must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '')) : true))\"\u003e \u003cgateway:standard:validation:XValidation:message=\"sectionName must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || (has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName))))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__)) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '') \u0026\u0026 (!has(p1.port) || p1.port == 0) == (!has(p2.port) || p2.port == 0)): true))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || ( has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName)) \u0026\u0026 (((!has(p1.port) || p1.port == 0) \u0026\u0026 (!has(p2.port) || p2.port == 0)) || (has(p1.port) \u0026\u0026 has(p2.port) \u0026\u0026 p1.port == p2.port))))\"\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.CookieConfig": {
                                                     "description": "CookieConfig defines the configuration for cookie-based session persistence.",
                                                     "type": "object",
                                                     "properties": {
                                                         "lifetimeType": {
                                                             "description": "LifetimeType specifies whether the cookie has a permanent or session-based lifetime. A permanent cookie persists until its specified expiry time, defined by the Expires or Max-Age cookie attributes, while a session cookie is deleted when the current session ends.\n\nWhen set to \"Permanent\", AbsoluteTimeout indicates the cookie's lifetime via the Expires or Max-Age cookie attributes and is required.\n\nWhen set to \"Session\", AbsoluteTimeout indicates the absolute lifetime of the cookie tracked by the gateway and is optional.\n\nSupport: Core for \"Session\" type\n\nSupport: Extended for \"Permanent\" type",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.FrontendTLSValidation": {
                                                     "description": "FrontendTLSValidation holds configuration information that can be used to validate the frontend initiating the TLS connection",
                                                     "type": "object",
                                                     "properties": {
                                                         "caCertificateRefs": {
                                                             "description": "CACertificateRefs contains one or more references to Kubernetes objects that contain TLS certificates of the Certificate Authorities that can be used as a trust anchor to validate the certificates presented by the client.\n\nA single CA certificate reference to a Kubernetes ConfigMap has \"Core\" support. Implementations MAY choose to support attaching multiple CA certificates to a Listener, but this behavior is implementation-specific.\n\nSupport: Core - A single reference to a Kubernetes ConfigMap with the CA certificate in a key named `ca.crt`.\n\nSupport: Implementation-specific (More than one reference, or other kinds of resources).\n\nReferences to a resource in a different namespace are invalid UNLESS there is a ReferenceGrant in the target namespace that allows the certificate to be attached. If a ReferenceGrant does not allow this reference, the \"ResolvedRefs\" condition MUST be set to False for this listener with the \"RefNotPermitted\" reason.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ObjectReference"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCBackendRef": {
                                                     "description": "GRPCBackendRef defines how a GRPCRoute forwards a gRPC request.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\n\u003cgateway:experimental:description\u003e\n\nWhen the BackendRef points to a Kubernetes Service, implementations SHOULD honor the appProtocol field if it is set for the target Service Port.\n\nImplementations supporting appProtocol SHOULD recognize the Kubernetes Standard Application Protocols defined in KEP-3726.\n\nIf a Service appProtocol isn't specified, an implementation MAY infer the backend protocol through its own means. Implementations MAY infer the protocol from the Route type referring to the backend Service.\n\nIf a Route is not able to send traffic to the backend using the specified protocol then the backend is considered invalid. Implementations MUST set the \"ResolvedRefs\" condition to \"False\" with the \"UnsupportedProtocol\" reason.\n\n\u003c/gateway:experimental:description\u003e",
                                                     "type": "object",
                                                     "required": [
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "filters": {
                                                             "description": "Filters defined at this level MUST be executed if and only if the request is being forwarded to the backend defined here.\n\nSupport: Implementation-specific (For broader support of filters, use the Filters field in GRPCRouteRule.)",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteFilter"
                                                             }
                                                         },
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the Kubernetes resource kind of the referent. For example \"Service\".\n\nDefaults to \"Service\" when not specified.\n\nExternalName services can refer to CNAME DNS records that may live outside of the cluster and as such are difficult to reason about in terms of conformance. They also may not be safe to forward to (see CVE-2021-25740 for more information). Implementations SHOULD NOT support ExternalName Services.\n\nSupport: Core (Services with a type other than ExternalName)\n\nSupport: Implementation-specific (Services with type ExternalName)",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the backend. When unspecified, the local namespace is inferred.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "port": {
                                                             "description": "Port specifies the destination port number to use for this resource. Port is required when the referent is a Kubernetes Service. In this case, the port number is the service port number, not the target port. For other resources, destination port might be derived from the referent resource or this field.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "weight": {
                                                             "description": "Weight specifies the proportion of requests forwarded to the referenced backend. This is computed as weight/(sum of all weights in this BackendRefs list). For non-zero values, there may be some epsilon from the exact proportion defined here depending on the precision an implementation supports. Weight is not a percentage and the sum of weights does not need to equal 100.\n\nIf only one backend is specified and it has a weight greater than 0, 100% of the traffic is forwarded to that backend. If weight is set to 0, no traffic should be forwarded for this entry. If unspecified, weight defaults to 1.\n\nSupport for this field varies based on the context where used.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCHeaderMatch": {
                                                     "description": "GRPCHeaderMatch describes how to select a gRPC route by matching gRPC request headers.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "value"
                                                     ],
                                                     "properties": {
                                                         "name": {
                                                             "description": "Name is the name of the gRPC Header to be matched.\n\nIf multiple entries specify equivalent header names, only the first entry with an equivalent name MUST be considered for a match. Subsequent entries with an equivalent header name MUST be ignored. Due to the case-insensitivity of header names, \"foo\" and \"Foo\" are considered equivalent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "type": {
                                                             "description": "Type specifies how to match against the value of the header.",
                                                             "type": "string"
                                                         },
                                                         "value": {
                                                             "description": "Value is the value of the gRPC Header to be matched.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCMethodMatch": {
                                                     "description": "GRPCMethodMatch describes how to select a gRPC route by matching the gRPC request service and/or method.\n\nAt least one of Service and Method MUST be a non-empty string.",
                                                     "type": "object",
                                                     "properties": {
                                                         "method": {
                                                             "description": "Value of the method to match against. If left empty or omitted, will match all services.\n\nAt least one of Service and Method MUST be a non-empty string.",
                                                             "type": "string"
                                                         },
                                                         "service": {
                                                             "description": "Value of the service to match against. If left empty or omitted, will match any service.\n\nAt least one of Service and Method MUST be a non-empty string.",
                                                             "type": "string"
                                                         },
                                                         "type": {
                                                             "description": "Type specifies how to match against the service and/or method. Support: Core (Exact with service and method specified)\n\nSupport: Implementation-specific (Exact with method specified but no service specified)\n\nSupport: Implementation-specific (RegularExpression)",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRoute": {
                                                     "description": "GRPCRoute provides a way to route gRPC requests. This includes the capability to match requests by hostname, gRPC service, gRPC method, or HTTP/2 header. Filters can be used to specify additional processing steps. Backends specify where matching requests will be routed.\n\nGRPCRoute falls under extended support within the Gateway API. Within the following specification, the word \"MUST\" indicates that an implementation supporting GRPCRoute must conform to the indicated requirement, but an implementation not supporting this route type need not follow the requirement unless explicitly indicated.\n\nImplementations supporting `GRPCRoute` with the `HTTPS` `ProtocolType` MUST accept HTTP/2 connections without an initial upgrade from HTTP/1.1, i.e. via ALPN. If the implementation does not support this, then it MUST set the \"Accepted\" condition to \"False\" for the affected listener with a reason of \"UnsupportedProtocol\".  Implementations MAY also accept HTTP/2 connections with an upgrade from HTTP/1.\n\nImplementations supporting `GRPCRoute` with the `HTTP` `ProtocolType` MUST support HTTP/2 over cleartext TCP (h2c, https://www.rfc-editor.org/rfc/rfc7540#section-3.1) without an initial upgrade from HTTP/1.1, i.e. with prior knowledge (https://www.rfc-editor.org/rfc/rfc7540#section-3.4). If the implementation does not support this, then it MUST set the \"Accepted\" condition to \"False\" for the affected listener with a reason of \"UnsupportedProtocol\". Implementations MAY also accept HTTP/2 connections with an upgrade from HTTP/1, i.e. without prior knowledge.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of GRPCRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of GRPCRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRouteFilter": {
                                                     "description": "GRPCRouteFilter defines processing steps that must be completed during the request or response lifecycle. GRPCRouteFilters are meant as an extension point to express processing that may be done in Gateway implementations. Some examples include request or response modification, implementing authentication strategies, rate-limiting, and traffic shaping. API guarantee/conformance is defined based on the type of the filter.",
                                                     "type": "object",
                                                     "required": [
                                                         "type"
                                                     ],
                                                     "properties": {
                                                         "extensionRef": {
                                                             "description": "ExtensionRef is an optional, implementation-specific extension to the \"filter\" behavior.  For example, resource \"myroutefilter\" in group \"networking.example.net\"). ExtensionRef MUST NOT be used for core and extended filters.\n\nSupport: Implementation-specific\n\nThis filter can be used multiple times within the same rule.",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.LocalObjectReference"
                                                         },
                                                         "requestHeaderModifier": {
                                                             "description": "RequestHeaderModifier defines a schema for a filter that modifies request headers.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderFilter"
                                                         },
                                                         "requestMirror": {
                                                             "description": "RequestMirror defines a schema for a filter that mirrors requests. Requests are sent to the specified destination, but responses from that destination are ignored.\n\nThis filter can be used multiple times within the same rule. Note that not all implementations will be able to support mirroring to multiple backends.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRequestMirrorFilter"
                                                         },
                                                         "responseHeaderModifier": {
                                                             "description": "ResponseHeaderModifier defines a schema for a filter that modifies response headers.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderFilter"
                                                         },
                                                         "type": {
                                                             "description": "Type identifies the type of filter to apply. As with other API fields, types are classified into three conformance levels:\n\n- Core: Filter types and their corresponding configuration defined by\n  \"Support: Core\" in this package, e.g. \"RequestHeaderModifier\". All\n  implementations supporting GRPCRoute MUST support core filters.\n\n- Extended: Filter types and their corresponding configuration defined by\n  \"Support: Extended\" in this package, e.g. \"RequestMirror\". Implementers\n  are encouraged to support extended filters.\n\n- Implementation-specific: Filters that are defined and supported by specific vendors.\n  In the future, filters showing convergence in behavior across multiple\n  implementations will be considered for inclusion in extended or core\n  conformance levels. Filter-specific configuration for such filters\n  is specified using the ExtensionRef field. `Type` MUST be set to\n  \"ExtensionRef\" for custom filters.\n\nImplementers are encouraged to define custom implementation types to extend the core API with implementation-specific behavior.\n\nIf a reference to a custom filter type cannot be resolved, the filter MUST NOT be skipped. Instead, requests that would have been processed by that filter MUST receive a HTTP error response.\n\n\u003cgateway:experimental:validation:Enum=ResponseHeaderModifier;RequestHeaderModifier;RequestMirror;ExtensionRef\u003e",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRouteList": {
                                                     "description": "GRPCRouteList contains a list of GRPCRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRouteMatch": {
                                                     "description": "GRPCRouteMatch defines the predicate used to match requests to a given action. Multiple match types are ANDed together, i.e. the match will evaluate to true only if all conditions are satisfied.\n\nFor example, the match below will match a gRPC request only if its service is `foo` AND it contains the `version: v1` header:\n\n``` matches:\n  - method:\n    type: Exact\n    service: \"foo\"\n    headers:\n  - name: \"version\"\n    value \"v1\"\n\n```",
                                                     "type": "object",
                                                     "properties": {
                                                         "headers": {
                                                             "description": "Headers specifies gRPC request header matchers. Multiple match values are ANDed together, meaning, a request MUST match all the specified headers to select the route.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCHeaderMatch"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "method": {
                                                             "description": "Method specifies a gRPC request service/method matcher. If this field is not specified, all services and methods will match.",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCMethodMatch"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRouteRule": {
                                                     "description": "GRPCRouteRule defines the semantics for matching a gRPC request based on conditions (matches), processing it (filters), and forwarding the request to an API object (backendRefs).",
                                                     "type": "object",
                                                     "properties": {
                                                         "backendRefs": {
                                                             "description": "BackendRefs defines the backend(s) where matching requests should be sent.\n\nFailure behavior here depends on how many BackendRefs are specified and how many are invalid.\n\nIf *all* entries in BackendRefs are invalid, and there are also no filters specified in this route rule, *all* traffic which matches this rule MUST receive an `UNAVAILABLE` status.\n\nSee the GRPCBackendRef definition for the rules about what makes a single GRPCBackendRef invalid.\n\nWhen a GRPCBackendRef is invalid, `UNAVAILABLE` statuses MUST be returned for requests that would have otherwise been routed to an invalid backend. If multiple backends are specified, and some are invalid, the proportion of requests that would otherwise have been routed to an invalid backend MUST receive an `UNAVAILABLE` status.\n\nFor example, if two backends are specified with equal weights, and one is invalid, 50 percent of traffic MUST receive an `UNAVAILABLE` status. Implementations may choose how that 50 percent is determined.\n\nSupport: Core for Kubernetes Service\n\nSupport: Implementation-specific for any other resource\n\nSupport for weight: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCBackendRef"
                                                             }
                                                         },
                                                         "filters": {
                                                             "description": "Filters define the filters that are applied to requests that match this rule.\n\nThe effects of ordering of multiple behaviors are currently unspecified. This can change in the future based on feedback during the alpha stage.\n\nConformance-levels at this level are defined based on the type of filter:\n\n- ALL core filters MUST be supported by all implementations that support\n  GRPCRoute.\n- Implementers are encouraged to support extended filters. - Implementation-specific custom filters have no API guarantees across\n  implementations.\n\nSpecifying the same filter multiple times is not supported unless explicitly indicated in the filter.\n\nIf an implementation can not support a combination of filters, it must clearly document that limitation. In cases where incompatible or unsupported filters are specified and cause the `Accepted` condition to be set to status `False`, implementations may use the `IncompatibleFilters` reason to specify this configuration error.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteFilter"
                                                             }
                                                         },
                                                         "matches": {
                                                             "description": "Matches define conditions used for matching the rule against incoming gRPC requests. Each match is independent, i.e. this rule will be matched if **any** one of the matches is satisfied.\n\nFor example, take the following matches configuration:\n\n``` matches: - method:\n    service: foo.bar\n  headers:\n    values:\n      version: 2\n- method:\n    service: foo.bar.v2\n```\n\nFor a request to match against this rule, it MUST satisfy EITHER of the two conditions:\n\n- service of foo.bar AND contains the header `version: 2` - service of foo.bar.v2\n\nSee the documentation for GRPCRouteMatch on how to specify multiple match conditions to be ANDed together.\n\nIf no matches are specified, the implementation MUST match every gRPC request.\n\nProxy or Load Balancer routing configuration generated from GRPCRoutes MUST prioritize rules based on the following criteria, continuing on ties. Merging MUST not be done between GRPCRoutes and HTTPRoutes. Precedence MUST be given to the rule with the largest number of:\n\n* Characters in a matching non-wildcard hostname. * Characters in a matching hostname. * Characters in a matching service. * Characters in a matching method. * Header matches.\n\nIf ties still exist across multiple Routes, matching precedence MUST be determined in order of the following criteria, continuing on ties:\n\n* The oldest Route based on creation timestamp. * The Route appearing first in alphabetical order by\n  \"{namespace}/{name}\".\n\nIf ties still exist within the Route that has been given precedence, matching precedence MUST be granted to the first matching rule meeting the above criteria.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteMatch"
                                                             }
                                                         },
                                                         "sessionPersistence": {
                                                             "description": "SessionPersistence defines and configures session persistence for the route rule.\n\nSupport: Extended\n\n\u003cgateway:experimental\u003e",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.SessionPersistence"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRouteSpec": {
                                                     "description": "GRPCRouteSpec defines the desired state of GRPCRoute",
                                                     "type": "object",
                                                     "properties": {
                                                         "hostnames": {
                                                             "description": "Hostnames defines a set of hostnames to match against the GRPC Host header to select a GRPCRoute to process the request. This matches the RFC 1123 definition of a hostname with 2 notable exceptions:\n\n1. IPs are not allowed. 2. A hostname may be prefixed with a wildcard label (`*.`). The wildcard\n   label MUST appear by itself as the first label.\n\nIf a hostname is specified by both the Listener and GRPCRoute, there MUST be at least one intersecting hostname for the GRPCRoute to be attached to the Listener. For example:\n\n* A Listener with `test.example.com` as the hostname matches GRPCRoutes\n  that have either not specified any hostnames, or have specified at\n  least one of `test.example.com` or `*.example.com`.\n* A Listener with `*.example.com` as the hostname matches GRPCRoutes\n  that have either not specified any hostnames or have specified at least\n  one hostname that matches the Listener hostname. For example,\n  `test.example.com` and `*.example.com` would both match. On the other\n  hand, `example.com` and `test.example.net` would not match.\n\nHostnames that are prefixed with a wildcard label (`*.`) are interpreted as a suffix match. That means that a match for `*.example.com` would match both `test.example.com`, and `foo.test.example.com`, but not `example.com`.\n\nIf both the Listener and GRPCRoute have specified hostnames, any GRPCRoute hostnames that do not match the Listener hostname MUST be ignored. For example, if a Listener specified `*.example.com`, and the GRPCRoute specified `test.example.com` and `test.example.net`, `test.example.net` MUST NOT be considered for a match.\n\nIf both the Listener and GRPCRoute have specified hostnames, and none match with the criteria above, then the GRPCRoute MUST NOT be accepted by the implementation. The implementation MUST raise an 'Accepted' Condition with a status of `False` in the corresponding RouteParentStatus.\n\nIf a Route (A) of type HTTPRoute or GRPCRoute is attached to a Listener and that listener already has another Route (B) of the other type attached and the intersection of the hostnames of A and B is non-empty, then the implementation MUST accept exactly one of these two routes, determined by the following criteria, in order:\n\n* The oldest Route based on creation timestamp. * The Route appearing first in alphabetical order by\n  \"{namespace}/{name}\".\n\nThe rejected Route MUST raise an 'Accepted' condition with a status of 'False' in the corresponding RouteParentStatus.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "parentRefs": {
                                                             "description": "ParentRefs references the resources (usually Gateways) that a Route wants to be attached to. Note that the referenced parent resource needs to allow this for the attachment to be complete. For Gateways, that means the Gateway needs to allow attachment from Routes of this kind and namespace. For Services, that means the Service must either be in the same namespace for a \"producer\" route, or the mesh implementation must support and allow \"consumer\" routes for the referenced Service. ReferenceGrant is not applicable for governing ParentRefs to Services - it is not possible to create a \"producer\" route for a Service in a different namespace from the Route.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nThis API may be extended in the future to support additional kinds of parent resources.\n\nParentRefs must be _distinct_. This means either that:\n\n* They select different objects.  If this is the case, then parentRef\n  entries are distinct. In terms of fields, this means that the\n  multi-part key defined by `group`, `kind`, `namespace`, and `name` must\n  be unique across all parentRef entries in the Route.\n* They do not select different objects, but for each optional field used,\n  each ParentRef that selects the same object must set the same set of\n  optional fields to different values. If one ParentRef sets a\n  combination of optional fields, all must set the same combination.\n\nSome examples:\n\n* If one ParentRef sets `sectionName`, all ParentRefs referencing the\n  same object must also set `sectionName`.\n* If one ParentRef sets `port`, all ParentRefs referencing the same\n  object must also set `port`.\n* If one ParentRef sets `sectionName` and `port`, all ParentRefs\n  referencing the same object must also set `sectionName` and `port`.\n\nIt is possible to separately reference multiple distinct objects that may be collapsed by an implementation. For example, some implementations may choose to merge compatible Gateway Listeners together. If that is the case, the list of routes attached to those resources should also be merged.\n\nNote that for ParentRefs that cross namespace boundaries, there are specific rules. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example, Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable other kinds of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\n\u003cgateway:standard:validation:XValidation:message=\"sectionName must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '')) : true))\"\u003e \u003cgateway:standard:validation:XValidation:message=\"sectionName must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || (has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName))))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__)) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '') \u0026\u0026 (!has(p1.port) || p1.port == 0) == (!has(p2.port) || p2.port == 0)): true))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || ( has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName)) \u0026\u0026 (((!has(p1.port) || p1.port == 0) \u0026\u0026 (!has(p2.port) || p2.port == 0)) || (has(p1.port) \u0026\u0026 has(p2.port) \u0026\u0026 p1.port == p2.port))))\"\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                             }
                                                         },
                                                         "rules": {
                                                             "description": "Rules are a list of GRPC matchers, filters and actions.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteRule"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GRPCRouteStatus": {
                                                     "description": "GRPCRouteStatus defines the observed state of GRPCRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "parents"
                                                     ],
                                                     "properties": {
                                                         "parents": {
                                                             "description": "Parents is a list of parent resources (usually Gateways) that are associated with the route, and the status of the route with respect to each parent. When this route attaches to a parent, the controller that manages the parent must add an entry to this list when the controller first sees the route and should update the entry as appropriate when the route or gateway is modified.\n\nNote that parent references that cannot be resolved by an implementation of this API will not be added to this list. Implementations of this API can only populate Route status for the Gateways/parent resources they are responsible for.\n\nA maximum of 32 Gateways will be represented in this list. An empty list means the route has not been attached to any Gateway.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.Gateway": {
                                                     "description": "Gateway represents an instance of a service-traffic handling infrastructure by binding Listeners to a set of IP addresses.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of Gateway.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewaySpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of Gateway.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayAddress": {
                                                     "description": "GatewayAddress describes an address that can be bound to a Gateway.",
                                                     "type": "object",
                                                     "required": [
                                                         "value"
                                                     ],
                                                     "properties": {
                                                         "type": {
                                                             "description": "Type of the address.",
                                                             "type": "string"
                                                         },
                                                         "value": {
                                                             "description": "Value of the address. The validity of the values will depend on the type and support by the controller.\n\nExamples: `1.2.3.4`, `128::1`, `my-ip-address`.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayClass": {
                                                     "description": "GatewayClass describes a class of Gateways available to the user for creating Gateway resources.\n\nIt is recommended that this resource be used as a template for Gateways. This means that a Gateway is based on the state of the GatewayClass at the time it was created and changes to the GatewayClass or associated parameters are not propagated down to existing Gateways. This recommendation is intended to limit the blast radius of changes to GatewayClass or associated parameters. If implementations choose to propagate GatewayClass changes to existing Gateways, that MUST be clearly documented by the implementation.\n\nWhenever one or more Gateways are using a GatewayClass, implementations SHOULD add the `gateway-exists-finalizer.gateway.networking.k8s.io` finalizer on the associated GatewayClass. This ensures that a GatewayClass associated with a Gateway is not deleted while in use.\n\nGatewayClass is a Cluster level resource.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of GatewayClass.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayClassSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of GatewayClass.\n\nImplementations MUST populate status on all GatewayClass resources which specify their controller name.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayClassStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayClassList": {
                                                     "description": "GatewayClassList contains a list of GatewayClass",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayClass"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayClassSpec": {
                                                     "description": "GatewayClassSpec reflects the configuration of a class of Gateways.",
                                                     "type": "object",
                                                     "required": [
                                                         "controllerName"
                                                     ],
                                                     "properties": {
                                                         "controllerName": {
                                                             "description": "ControllerName is the name of the controller that is managing Gateways of this class. The value of this field MUST be a domain prefixed path.\n\nExample: \"example.net/gateway-controller\".\n\nThis field is not mutable and cannot be empty.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "description": {
                                                             "description": "Description helps describe a GatewayClass with more details.",
                                                             "type": "string"
                                                         },
                                                         "parametersRef": {
                                                             "description": "ParametersRef is a reference to a resource that contains the configuration parameters corresponding to the GatewayClass. This is optional if the controller does not require any additional configuration.\n\nParametersRef can reference a standard Kubernetes resource, i.e. ConfigMap, or an implementation-specific custom resource. The resource can be cluster-scoped or namespace-scoped.\n\nIf the referent cannot be found, refers to an unsupported kind, or when the data within that resource is malformed, the GatewayClass SHOULD be rejected with the \"Accepted\" status condition set to \"False\" and an \"InvalidParameters\" reason.\n\nA Gateway for this GatewayClass may provide its own `parametersRef`. When both are specified, the merging behavior is implementation specific. It is generally recommended that GatewayClass provides defaults that can be overridden by a Gateway.\n\nSupport: Implementation-specific",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParametersReference"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayClassStatus": {
                                                     "description": "GatewayClassStatus is the current status for the GatewayClass.",
                                                     "type": "object",
                                                     "properties": {
                                                         "conditions": {
                                                             "description": "Conditions is the current status from the controller for this GatewayClass.\n\nControllers should prefer to publish conditions using values of GatewayClassConditionType for the type of each Condition.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Condition"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "type"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "supportedFeatures": {
                                                             "description": "SupportedFeatures is the set of features the GatewayClass support. It MUST be sorted in ascending alphabetical order. \u003cgateway:experimental\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "set"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayInfrastructure": {
                                                     "description": "GatewayInfrastructure defines infrastructure level attributes about a Gateway instance.",
                                                     "type": "object",
                                                     "properties": {
                                                         "annotations": {
                                                             "description": "Annotations that SHOULD be applied to any resources created in response to this Gateway.\n\nFor implementations creating other Kubernetes objects, this should be the `metadata.annotations` field on resources. For other implementations, this refers to any relevant (implementation specific) \"annotations\" concepts.\n\nAn implementation may chose to add additional implementation-specific annotations as they see fit.\n\nSupport: Extended",
                                                             "type": "object",
                                                             "additionalProperties": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "labels": {
                                                             "description": "Labels that SHOULD be applied to any resources created in response to this Gateway.\n\nFor implementations creating other Kubernetes objects, this should be the `metadata.labels` field on resources. For other implementations, this refers to any relevant (implementation specific) \"labels\" concepts.\n\nAn implementation may chose to add additional implementation-specific labels as they see fit.\n\nSupport: Extended",
                                                             "type": "object",
                                                             "additionalProperties": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "parametersRef": {
                                                             "description": "ParametersRef is a reference to a resource that contains the configuration parameters corresponding to the Gateway. This is optional if the controller does not require any additional configuration.\n\nThis follows the same semantics as GatewayClass's `parametersRef`, but on a per-Gateway basis\n\nThe Gateway's GatewayClass may provide its own `parametersRef`. When both are specified, the merging behavior is implementation specific. It is generally recommended that GatewayClass provides defaults that can be overridden by a Gateway.\n\nSupport: Implementation-specific",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.LocalParametersReference"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayList": {
                                                     "description": "GatewayList contains a list of Gateways.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.Gateway"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewaySpec": {
                                                     "description": "GatewaySpec defines the desired state of Gateway.\n\nNot all possible combinations of options specified in the Spec are valid. Some invalid configurations can be caught synchronously via CRD validation, but there are many cases that will require asynchronous signaling via the GatewayStatus block.",
                                                     "type": "object",
                                                     "required": [
                                                         "gatewayClassName",
                                                         "listeners"
                                                     ],
                                                     "properties": {
                                                         "addresses": {
                                                             "description": "Addresses requested for this Gateway. This is optional and behavior can depend on the implementation. If a value is set in the spec and the requested address is invalid or unavailable, the implementation MUST indicate this in the associated entry in GatewayStatus.Addresses.\n\nThe Addresses field represents a request for the address(es) on the \"outside of the Gateway\", that traffic bound for this Gateway will use. This could be the IP address or hostname of an external load balancer or other networking infrastructure, or some other address that traffic will be sent to.\n\nIf no Addresses are specified, the implementation MAY schedule the Gateway in an implementation-specific manner, assigning an appropriate set of Addresses.\n\nThe implementation MUST bind all Listeners to every GatewayAddress that it assigns to the Gateway and add a corresponding entry in GatewayStatus.Addresses.\n\nSupport: Extended\n\n\u003cgateway:validateIPAddress\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayAddress"
                                                             }
                                                         },
                                                         "gatewayClassName": {
                                                             "description": "GatewayClassName used for this Gateway. This is the name of a GatewayClass resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "infrastructure": {
                                                             "description": "Infrastructure defines infrastructure level attributes about this Gateway instance.\n\nSupport: Core\n\n\u003cgateway:experimental\u003e",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayInfrastructure"
                                                         },
                                                         "listeners": {
                                                             "description": "Listeners associated with this Gateway. Listeners define logical endpoints that are bound on this Gateway's addresses. At least one Listener MUST be specified.\n\nEach Listener in a set of Listeners (for example, in a single Gateway) MUST be _distinct_, in that a traffic flow MUST be able to be assigned to exactly one listener. (This section uses \"set of Listeners\" rather than \"Listeners in a single Gateway\" because implementations MAY merge configuration from multiple Gateways onto a single data plane, and these rules _also_ apply in that case).\n\nPractically, this means that each listener in a set MUST have a unique combination of Port, Protocol, and, if supported by the protocol, Hostname.\n\nSome combinations of port, protocol, and TLS settings are considered Core support and MUST be supported by implementations based on their targeted conformance profile:\n\nHTTP Profile\n\n1. HTTPRoute, Port: 80, Protocol: HTTP 2. HTTPRoute, Port: 443, Protocol: HTTPS, TLS Mode: Terminate, TLS keypair provided\n\nTLS Profile\n\n1. TLSRoute, Port: 443, Protocol: TLS, TLS Mode: Passthrough\n\n\"Distinct\" Listeners have the following property:\n\nThe implementation can match inbound requests to a single distinct Listener. When multiple Listeners share values for fields (for example, two Listeners with the same Port value), the implementation can match requests to only one of the Listeners using other Listener fields.\n\nFor example, the following Listener scenarios are distinct:\n\n1. Multiple Listeners with the same Port that all use the \"HTTP\"\n   Protocol that all have unique Hostname values.\n2. Multiple Listeners with the same Port that use either the \"HTTPS\" or\n   \"TLS\" Protocol that all have unique Hostname values.\n3. A mixture of \"TCP\" and \"UDP\" Protocol Listeners, where no Listener\n   with the same Protocol has the same Port value.\n\nSome fields in the Listener struct have possible values that affect whether the Listener is distinct. Hostname is particularly relevant for HTTP or HTTPS protocols.\n\nWhen using the Hostname value to select between same-Port, same-Protocol Listeners, the Hostname value must be different on each Listener for the Listener to be distinct.\n\nWhen the Listeners are distinct based on Hostname, inbound request hostnames MUST match from the most specific to least specific Hostname values to choose the correct Listener and its associated set of Routes.\n\nExact matches must be processed before wildcard matches, and wildcard matches must be processed before fallback (empty Hostname value) matches. For example, `\"foo.example.com\"` takes precedence over `\"*.example.com\"`, and `\"*.example.com\"` takes precedence over `\"\"`.\n\nAdditionally, if there are multiple wildcard entries, more specific wildcard entries must be processed before less specific wildcard entries. For example, `\"*.foo.example.com\"` takes precedence over `\"*.example.com\"`. The precise definition here is that the higher the number of dots in the hostname to the right of the wildcard character, the higher the precedence.\n\nThe wildcard character will match any number of characters _and dots_ to the left, however, so `\"*.example.com\"` will match both `\"foo.bar.example.com\"` _and_ `\"bar.example.com\"`.\n\nIf a set of Listeners contains Listeners that are not distinct, then those Listeners are Conflicted, and the implementation MUST set the \"Conflicted\" condition in the Listener Status to \"True\".\n\nImplementations MAY choose to accept a Gateway with some Conflicted Listeners only if they only accept the partial Listener set that contains no Conflicted Listeners. To put this another way, implementations may accept a partial Listener set only if they throw out *all* the conflicting Listeners. No picking one of the conflicting listeners as the winner. This also means that the Gateway must have at least one non-conflicting Listener in this case, otherwise it violates the requirement that at least one Listener must be present.\n\nThe implementation MUST set a \"ListenersNotValid\" condition on the Gateway Status when the Gateway contains Conflicted Listeners whether or not they accept the Gateway. That Condition SHOULD clearly indicate in the Message which Listeners are conflicted, and which are Accepted. Additionally, the Listener status for those listeners SHOULD indicate which Listeners are conflicted and not Accepted.\n\nA Gateway's Listeners are considered \"compatible\" if:\n\n1. They are distinct. 2. The implementation can serve them in compliance with the Addresses\n   requirement that all Listeners are available on all assigned\n   addresses.\n\nCompatible combinations in Extended support are expected to vary across implementations. A combination that is compatible for one implementation may not be compatible for another.\n\nFor example, an implementation that cannot serve both TCP and UDP listeners on the same address, or cannot mix HTTPS and generic TLS listens on the same port would not consider those cases compatible, even though they are distinct.\n\nNote that requests SHOULD match at most one Listener. For example, if Listeners are defined for \"foo.example.com\" and \"*.example.com\", a request to \"foo.example.com\" SHOULD only be routed using routes attached to the \"foo.example.com\" Listener (and not the \"*.example.com\" Listener). This concept is known as \"Listener Isolation\". Implementations that do not support Listener Isolation MUST clearly document this.\n\nImplementations MAY merge separate Gateways onto a single set of Addresses if all Listeners across all Gateways are compatible.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.Listener"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayStatus": {
                                                     "description": "GatewayStatus defines the observed state of Gateway.",
                                                     "type": "object",
                                                     "properties": {
                                                         "addresses": {
                                                             "description": "Addresses lists the network addresses that have been bound to the Gateway.\n\nThis list may differ from the addresses provided in the spec under some conditions:\n\n  * no addresses are specified, all addresses are dynamically assigned\n  * a combination of specified and dynamic addresses are assigned\n  * a specified address was unusable (e.g. already in use)\n\n\u003cgateway:validateIPAddress\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayStatusAddress"
                                                             }
                                                         },
                                                         "conditions": {
                                                             "description": "Conditions describe the current conditions of the Gateway.\n\nImplementations should prefer to express Gateway conditions using the `GatewayConditionType` and `GatewayConditionReason` constants so that operators and tools can converge on a common vocabulary to describe Gateway state.\n\nKnown condition types are:\n\n* \"Accepted\" * \"Programmed\" * \"Ready\"",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Condition"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "type"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "listeners": {
                                                             "description": "Listeners provide status for each unique listener port defined in the Spec.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ListenerStatus"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayStatusAddress": {
                                                     "description": "GatewayStatusAddress describes a network address that is bound to a Gateway.",
                                                     "type": "object",
                                                     "required": [
                                                         "value"
                                                     ],
                                                     "properties": {
                                                         "type": {
                                                             "description": "Type of the address.",
                                                             "type": "string"
                                                         },
                                                         "value": {
                                                             "description": "Value of the address. The validity of the values will depend on the type and support by the controller.\n\nExamples: `1.2.3.4`, `128::1`, `my-ip-address`.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.GatewayTLSConfig": {
                                                     "description": "GatewayTLSConfig describes a TLS configuration.",
                                                     "type": "object",
                                                     "properties": {
                                                         "certificateRefs": {
                                                             "description": "CertificateRefs contains a series of references to Kubernetes objects that contains TLS certificates and private keys. These certificates are used to establish a TLS handshake for requests that match the hostname of the associated listener.\n\nA single CertificateRef to a Kubernetes Secret has \"Core\" support. Implementations MAY choose to support attaching multiple certificates to a Listener, but this behavior is implementation-specific.\n\nReferences to a resource in different namespace are invalid UNLESS there is a ReferenceGrant in the target namespace that allows the certificate to be attached. If a ReferenceGrant does not allow this reference, the \"ResolvedRefs\" condition MUST be set to False for this listener with the \"RefNotPermitted\" reason.\n\nThis field is required to have at least one element when the mode is set to \"Terminate\" (default) and is optional otherwise.\n\nCertificateRefs can reference to standard Kubernetes resources, i.e. Secret, or implementation-specific custom resources.\n\nSupport: Core - A single reference to a Kubernetes Secret of type kubernetes.io/tls\n\nSupport: Implementation-specific (More than one reference or other resource types)",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.SecretObjectReference"
                                                             }
                                                         },
                                                         "frontendValidation": {
                                                             "description": "FrontendValidation holds configuration information for validating the frontend (client). Setting this field will require clients to send a client certificate required for validation during the TLS handshake. In browsers this may result in a dialog appearing that requests a user to specify the client certificate. The maximum depth of a certificate chain accepted in verification is Implementation specific.\n\nSupport: Extended\n\n\u003cgateway:experimental\u003e",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.FrontendTLSValidation"
                                                         },
                                                         "mode": {
                                                             "description": "Mode defines the TLS behavior for the TLS session initiated by the client. There are two possible modes:\n\n- Terminate: The TLS session between the downstream client and the\n  Gateway is terminated at the Gateway. This mode requires certificates\n  to be specified in some way, such as populating the certificateRefs\n  field.\n- Passthrough: The TLS session is NOT terminated by the Gateway. This\n  implies that the Gateway can't decipher the TLS stream except for\n  the ClientHello message of the TLS protocol. The certificateRefs field\n  is ignored in this mode.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "options": {
                                                             "description": "Options are a list of key/value pairs to enable extended TLS configuration for each implementation. For example, configuring the minimum TLS version or supported cipher suites.\n\nA set of common keys MAY be defined by the API in the future. To avoid any ambiguity, implementation-specific definitions MUST use domain-prefixed names, such as `example.com/my-custom-option`. Un-prefixed names are reserved for key names defined by Gateway API.\n\nSupport: Implementation-specific",
                                                             "type": "object",
                                                             "additionalProperties": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPBackendRef": {
                                                     "description": "HTTPBackendRef defines how a HTTPRoute should forward an HTTP request.",
                                                     "type": "object",
                                                     "required": [
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "filters": {
                                                             "description": "Filters defined at this level should be executed if and only if the request is being forwarded to the backend defined here.\n\nSupport: Implementation-specific (For broader support of filters, use the Filters field in HTTPRouteRule.)",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteFilter"
                                                             }
                                                         },
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the Kubernetes resource kind of the referent. For example \"Service\".\n\nDefaults to \"Service\" when not specified.\n\nExternalName services can refer to CNAME DNS records that may live outside of the cluster and as such are difficult to reason about in terms of conformance. They also may not be safe to forward to (see CVE-2021-25740 for more information). Implementations SHOULD NOT support ExternalName Services.\n\nSupport: Core (Services with a type other than ExternalName)\n\nSupport: Implementation-specific (Services with type ExternalName)",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the backend. When unspecified, the local namespace is inferred.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "port": {
                                                             "description": "Port specifies the destination port number to use for this resource. Port is required when the referent is a Kubernetes Service. In this case, the port number is the service port number, not the target port. For other resources, destination port might be derived from the referent resource or this field.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "weight": {
                                                             "description": "Weight specifies the proportion of requests forwarded to the referenced backend. This is computed as weight/(sum of all weights in this BackendRefs list). For non-zero values, there may be some epsilon from the exact proportion defined here depending on the precision an implementation supports. Weight is not a percentage and the sum of weights does not need to equal 100.\n\nIf only one backend is specified and it has a weight greater than 0, 100% of the traffic is forwarded to that backend. If weight is set to 0, no traffic should be forwarded for this entry. If unspecified, weight defaults to 1.\n\nSupport for this field varies based on the context where used.",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPHeader": {
                                                     "description": "HTTPHeader represents an HTTP Header name and value as defined by RFC 7230.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "value"
                                                     ],
                                                     "properties": {
                                                         "name": {
                                                             "description": "Name is the name of the HTTP Header to be matched. Name matching MUST be case insensitive. (See https://tools.ietf.org/html/rfc7230#section-3.2).\n\nIf multiple entries specify equivalent header names, the first entry with an equivalent name MUST be considered for a match. Subsequent entries with an equivalent header name MUST be ignored. Due to the case-insensitivity of header names, \"foo\" and \"Foo\" are considered equivalent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "value": {
                                                             "description": "Value is the value of HTTP Header to be matched.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderFilter": {
                                                     "description": "HTTPHeaderFilter defines a filter that modifies the headers of an HTTP request or response.",
                                                     "type": "object",
                                                     "properties": {
                                                         "add": {
                                                             "description": "Add adds the given header(s) (name, value) to the request before the action. It appends to any existing values associated with the header name.\n\nInput:\n  GET /foo HTTP/1.1\n  my-header: foo\n\nConfig:\n  add:\n  - name: \"my-header\"\n    value: \"bar,baz\"\n\nOutput:\n  GET /foo HTTP/1.1\n  my-header: foo,bar,baz",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeader"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "remove": {
                                                             "description": "Remove the given header(s) from the HTTP request before the action. The value of Remove is a list of HTTP header names. Note that the header names are case-insensitive (see https://datatracker.ietf.org/doc/html/rfc2616#section-4.2).\n\nInput:\n  GET /foo HTTP/1.1\n  my-header1: foo\n  my-header2: bar\n  my-header3: baz\n\nConfig:\n  remove: [\"my-header1\", \"my-header3\"]\n\nOutput:\n  GET /foo HTTP/1.1\n  my-header2: bar",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             },
                                                             "x-kubernetes-list-type": "set"
                                                         },
                                                         "set": {
                                                             "description": "Set overwrites the request with the given header (name, value) before the action.\n\nInput:\n  GET /foo HTTP/1.1\n  my-header: foo\n\nConfig:\n  set:\n  - name: \"my-header\"\n    value: \"bar\"\n\nOutput:\n  GET /foo HTTP/1.1\n  my-header: bar",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeader"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderMatch": {
                                                     "description": "HTTPHeaderMatch describes how to select a HTTP route by matching HTTP request headers.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "value"
                                                     ],
                                                     "properties": {
                                                         "name": {
                                                             "description": "Name is the name of the HTTP Header to be matched. Name matching MUST be case insensitive. (See https://tools.ietf.org/html/rfc7230#section-3.2).\n\nIf multiple entries specify equivalent header names, only the first entry with an equivalent name MUST be considered for a match. Subsequent entries with an equivalent header name MUST be ignored. Due to the case-insensitivity of header names, \"foo\" and \"Foo\" are considered equivalent.\n\nWhen a header is repeated in an HTTP request, it is implementation-specific behavior as to how this is represented. Generally, proxies should follow the guidance from the RFC: https://www.rfc-editor.org/rfc/rfc7230.html#section-3.2.2 regarding processing a repeated header, with special handling for \"Set-Cookie\".",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "type": {
                                                             "description": "Type specifies how to match against the value of the header.\n\nSupport: Core (Exact)\n\nSupport: Implementation-specific (RegularExpression)\n\nSince RegularExpression HeaderMatchType has implementation-specific conformance, implementations can support POSIX, PCRE or any other dialects of regular expressions. Please read the implementation's documentation to determine the supported dialect.",
                                                             "type": "string"
                                                         },
                                                         "value": {
                                                             "description": "Value is the value of HTTP Header to be matched.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPPathMatch": {
                                                     "description": "HTTPPathMatch describes how to select a HTTP route by matching the HTTP request path.",
                                                     "type": "object",
                                                     "properties": {
                                                         "type": {
                                                             "description": "Type specifies how to match against the path Value.\n\nSupport: Core (Exact, PathPrefix)\n\nSupport: Implementation-specific (RegularExpression)",
                                                             "type": "string"
                                                         },
                                                         "value": {
                                                             "description": "Value of the HTTP path to match against.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPPathModifier": {
                                                     "description": "HTTPPathModifier defines configuration for path modifiers. \u003cgateway:experimental\u003e",
                                                     "type": "object",
                                                     "required": [
                                                         "type"
                                                     ],
                                                     "properties": {
                                                         "replaceFullPath": {
                                                             "description": "ReplaceFullPath specifies the value with which to replace the full path of a request during a rewrite or redirect.",
                                                             "type": "string"
                                                         },
                                                         "replacePrefixMatch": {
                                                             "description": "ReplacePrefixMatch specifies the value with which to replace the prefix match of a request during a rewrite or redirect. For example, a request to \"/foo/bar\" with a prefix match of \"/foo\" and a ReplacePrefixMatch of \"/xyz\" would be modified to \"/xyz/bar\".\n\nNote that this matches the behavior of the PathPrefix match type. This matches full path elements. A path element refers to the list of labels in the path split by the `/` separator. When specified, a trailing `/` is ignored. For example, the paths `/abc`, `/abc/`, and `/abc/def` would all match the prefix `/abc`, but the path `/abcd` would not.\n\nReplacePrefixMatch is only compatible with a `PathPrefix` HTTPRouteMatch. Using any other HTTPRouteMatch type on the same HTTPRouteRule will result in the implementation setting the Accepted Condition for the Route to `status: False`.\n\nRequest Path | Prefix Match | Replace Prefix | Modified Path -------------|--------------|----------------|---------- /foo/bar     | /foo         | /xyz           | /xyz/bar /foo/bar     | /foo         | /xyz/          | /xyz/bar /foo/bar     | /foo/        | /xyz           | /xyz/bar /foo/bar     | /foo/        | /xyz/          | /xyz/bar /foo         | /foo         | /xyz           | /xyz /foo/        | /foo         | /xyz           | /xyz/ /foo/bar     | /foo         | \u003cempty string\u003e | /bar /foo/        | /foo         | \u003cempty string\u003e | / /foo         | /foo         | \u003cempty string\u003e | / /foo/        | /foo         | /              | / /foo         | /foo         | /              | /",
                                                             "type": "string"
                                                         },
                                                         "type": {
                                                             "description": "Type defines the type of path modifier. Additional types may be added in a future release of the API.\n\nNote that values may be added to this enum, implementations must ensure that unknown values will not cause a crash.\n\nUnknown values here must result in the implementation setting the Accepted Condition for the Route to `status: False`, with a Reason of `UnsupportedValue`.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPQueryParamMatch": {
                                                     "description": "HTTPQueryParamMatch describes how to select a HTTP route by matching HTTP query parameters.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "value"
                                                     ],
                                                     "properties": {
                                                         "name": {
                                                             "description": "Name is the name of the HTTP query param to be matched. This must be an exact string match. (See https://tools.ietf.org/html/rfc7230#section-2.7.3).\n\nIf multiple entries specify equivalent query param names, only the first entry with an equivalent name MUST be considered for a match. Subsequent entries with an equivalent query param name MUST be ignored.\n\nIf a query param is repeated in an HTTP request, the behavior is purposely left undefined, since different data planes have different capabilities. However, it is *recommended* that implementations should match against the first value of the param if the data plane supports it, as this behavior is expected in other load balancing contexts outside of the Gateway API.\n\nUsers SHOULD NOT route traffic based on repeated query params to guard themselves against potential differences in the implementations.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "type": {
                                                             "description": "Type specifies how to match against the value of the query parameter.\n\nSupport: Extended (Exact)\n\nSupport: Implementation-specific (RegularExpression)\n\nSince RegularExpression QueryParamMatchType has Implementation-specific conformance, implementations can support POSIX, PCRE or any other dialects of regular expressions. Please read the implementation's documentation to determine the supported dialect.",
                                                             "type": "string"
                                                         },
                                                         "value": {
                                                             "description": "Value is the value of HTTP query param to be matched.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRequestMirrorFilter": {
                                                     "description": "HTTPRequestMirrorFilter defines configuration for the RequestMirror filter.",
                                                     "type": "object",
                                                     "required": [
                                                         "backendRef"
                                                     ],
                                                     "properties": {
                                                         "backendRef": {
                                                             "description": "BackendRef references a resource where mirrored requests are sent.\n\nMirrored requests must be sent only to a single destination endpoint within this BackendRef, irrespective of how many endpoints are present within this BackendRef.\n\nIf the referent cannot be found, this BackendRef is invalid and must be dropped from the Gateway. The controller must ensure the \"ResolvedRefs\" condition on the Route status is set to `status: False` and not configure this backend in the underlying implementation.\n\nIf there is a cross-namespace reference to an *existing* object that is not allowed by a ReferenceGrant, the controller must ensure the \"ResolvedRefs\"  condition on the Route is set to `status: False`, with the \"RefNotPermitted\" reason and not configure this backend in the underlying implementation.\n\nIn either error case, the Message of the `ResolvedRefs` Condition should be used to provide more detail about the problem.\n\nSupport: Extended for Kubernetes Service\n\nSupport: Implementation-specific for any other resource",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.BackendObjectReference"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRequestRedirectFilter": {
                                                     "description": "HTTPRequestRedirect defines a filter that redirects a request. This filter MUST NOT be used on the same Route rule as a HTTPURLRewrite filter.",
                                                     "type": "object",
                                                     "properties": {
                                                         "hostname": {
                                                             "description": "Hostname is the hostname to be used in the value of the `Location` header in the response. When empty, the hostname in the `Host` header of the request is used.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "path": {
                                                             "description": "Path defines parameters used to modify the path of the incoming request. The modified path is then used to construct the `Location` header. When empty, the request path is used as-is.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPPathModifier"
                                                         },
                                                         "port": {
                                                             "description": "Port is the port to be used in the value of the `Location` header in the response.\n\nIf no port is specified, the redirect port MUST be derived using the following rules:\n\n* If redirect scheme is not-empty, the redirect port MUST be the well-known\n  port associated with the redirect scheme. Specifically \"http\" to port 80\n  and \"https\" to port 443. If the redirect scheme does not have a\n  well-known port, the listener port of the Gateway SHOULD be used.\n* If redirect scheme is empty, the redirect port MUST be the Gateway\n  Listener port.\n\nImplementations SHOULD NOT add the port number in the 'Location' header in the following cases:\n\n* A Location header that will use HTTP (whether that is determined via\n  the Listener protocol or the Scheme field) _and_ use port 80.\n* A Location header that will use HTTPS (whether that is determined via\n  the Listener protocol or the Scheme field) _and_ use port 443.\n\nSupport: Extended",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "scheme": {
                                                             "description": "Scheme is the scheme to be used in the value of the `Location` header in the response. When empty, the scheme of the request is used.\n\nScheme redirects can affect the port of the redirect, for more information, refer to the documentation for the port field of this filter.\n\nNote that values may be added to this enum, implementations must ensure that unknown values will not cause a crash.\n\nUnknown values here must result in the implementation setting the Accepted Condition for the Route to `status: False`, with a Reason of `UnsupportedValue`.\n\nSupport: Extended",
                                                             "type": "string"
                                                         },
                                                         "statusCode": {
                                                             "description": "StatusCode is the HTTP status code to be used in response.\n\nNote that values may be added to this enum, implementations must ensure that unknown values will not cause a crash.\n\nUnknown values here must result in the implementation setting the Accepted Condition for the Route to `status: False`, with a Reason of `UnsupportedValue`.\n\nSupport: Core",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRoute": {
                                                     "description": "HTTPRoute provides a way to route HTTP requests. This includes the capability to match requests by hostname, path, header, or query param. Filters can be used to specify additional processing steps. Backends specify where matching requests should be routed.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of HTTPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of HTTPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteFilter": {
                                                     "description": "HTTPRouteFilter defines processing steps that must be completed during the request or response lifecycle. HTTPRouteFilters are meant as an extension point to express processing that may be done in Gateway implementations. Some examples include request or response modification, implementing authentication strategies, rate-limiting, and traffic shaping. API guarantee/conformance is defined based on the type of the filter.",
                                                     "type": "object",
                                                     "required": [
                                                         "type"
                                                     ],
                                                     "properties": {
                                                         "extensionRef": {
                                                             "description": "ExtensionRef is an optional, implementation-specific extension to the \"filter\" behavior.  For example, resource \"myroutefilter\" in group \"networking.example.net\"). ExtensionRef MUST NOT be used for core and extended filters.\n\nThis filter can be used multiple times within the same rule.\n\nSupport: Implementation-specific",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.LocalObjectReference"
                                                         },
                                                         "requestHeaderModifier": {
                                                             "description": "RequestHeaderModifier defines a schema for a filter that modifies request headers.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderFilter"
                                                         },
                                                         "requestMirror": {
                                                             "description": "RequestMirror defines a schema for a filter that mirrors requests. Requests are sent to the specified destination, but responses from that destination are ignored.\n\nThis filter can be used multiple times within the same rule. Note that not all implementations will be able to support mirroring to multiple backends.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRequestMirrorFilter"
                                                         },
                                                         "requestRedirect": {
                                                             "description": "RequestRedirect defines a schema for a filter that responds to the request with an HTTP redirection.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRequestRedirectFilter"
                                                         },
                                                         "responseHeaderModifier": {
                                                             "description": "ResponseHeaderModifier defines a schema for a filter that modifies response headers.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderFilter"
                                                         },
                                                         "type": {
                                                             "description": "Type identifies the type of filter to apply. As with other API fields, types are classified into three conformance levels:\n\n- Core: Filter types and their corresponding configuration defined by\n  \"Support: Core\" in this package, e.g. \"RequestHeaderModifier\". All\n  implementations must support core filters.\n\n- Extended: Filter types and their corresponding configuration defined by\n  \"Support: Extended\" in this package, e.g. \"RequestMirror\". Implementers\n  are encouraged to support extended filters.\n\n- Implementation-specific: Filters that are defined and supported by\n  specific vendors.\n  In the future, filters showing convergence in behavior across multiple\n  implementations will be considered for inclusion in extended or core\n  conformance levels. Filter-specific configuration for such filters\n  is specified using the ExtensionRef field. `Type` should be set to\n  \"ExtensionRef\" for custom filters.\n\nImplementers are encouraged to define custom implementation types to extend the core API with implementation-specific behavior.\n\nIf a reference to a custom filter type cannot be resolved, the filter MUST NOT be skipped. Instead, requests that would have been processed by that filter MUST receive a HTTP error response.\n\nNote that values may be added to this enum, implementations must ensure that unknown values will not cause a crash.\n\nUnknown values here must result in the implementation setting the Accepted Condition for the Route to `status: False`, with a Reason of `UnsupportedValue`.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "urlRewrite": {
                                                             "description": "URLRewrite defines a schema for a filter that modifies a request during forwarding.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPURLRewriteFilter"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteList": {
                                                     "description": "HTTPRouteList contains a list of HTTPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteMatch": {
                                                     "description": "HTTPRouteMatch defines the predicate used to match requests to a given action. Multiple match types are ANDed together, i.e. the match will evaluate to true only if all conditions are satisfied.\n\nFor example, the match below will match a HTTP request only if its path starts with `/foo` AND it contains the `version: v1` header:\n\n``` match:\n\n\tpath:\n\t  value: \"/foo\"\n\theaders:\n\t- name: \"version\"\n\t  value \"v1\"\n\n```",
                                                     "type": "object",
                                                     "properties": {
                                                         "headers": {
                                                             "description": "Headers specifies HTTP request header matchers. Multiple match values are ANDed together, meaning, a request must match all the specified headers to select the route.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPHeaderMatch"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "method": {
                                                             "description": "Method specifies HTTP method matcher. When specified, this route will be matched only if the request has the specified method.\n\nSupport: Extended",
                                                             "type": "string"
                                                         },
                                                         "path": {
                                                             "description": "Path specifies a HTTP request path matcher. If this field is not specified, a default prefix match on the \"/\" path is provided.",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPPathMatch"
                                                         },
                                                         "queryParams": {
                                                             "description": "QueryParams specifies HTTP query parameter matchers. Multiple match values are ANDed together, meaning, a request must match all the specified query parameters to select the route.\n\nSupport: Extended",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPQueryParamMatch"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteRule": {
                                                     "description": "HTTPRouteRule defines semantics for matching an HTTP request based on conditions (matches), processing it (filters), and forwarding the request to an API object (backendRefs).",
                                                     "type": "object",
                                                     "properties": {
                                                         "backendRefs": {
                                                             "description": "BackendRefs defines the backend(s) where matching requests should be sent.\n\nFailure behavior here depends on how many BackendRefs are specified and how many are invalid.\n\nIf *all* entries in BackendRefs are invalid, and there are also no filters specified in this route rule, *all* traffic which matches this rule MUST receive a 500 status code.\n\nSee the HTTPBackendRef definition for the rules about what makes a single HTTPBackendRef invalid.\n\nWhen a HTTPBackendRef is invalid, 500 status codes MUST be returned for requests that would have otherwise been routed to an invalid backend. If multiple backends are specified, and some are invalid, the proportion of requests that would otherwise have been routed to an invalid backend MUST receive a 500 status code.\n\nFor example, if two backends are specified with equal weights, and one is invalid, 50 percent of traffic must receive a 500. Implementations may choose how that 50 percent is determined.\n\nWhen a HTTPBackendRef refers to a Service that has no ready endpoints, implementations SHOULD return a 503 for requests to that backend instead. If an implementation chooses to do this, all of the above rules for 500 responses MUST also apply for responses that return a 503.\n\nSupport: Core for Kubernetes Service\n\nSupport: Extended for Kubernetes ServiceImport\n\nSupport: Implementation-specific for any other resource\n\nSupport for weight: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPBackendRef"
                                                             }
                                                         },
                                                         "filters": {
                                                             "description": "Filters define the filters that are applied to requests that match this rule.\n\nWherever possible, implementations SHOULD implement filters in the order they are specified.\n\nImplementations MAY choose to implement this ordering strictly, rejecting any combination or order of filters that can not be supported. If implementations choose a strict interpretation of filter ordering, they MUST clearly document that behavior.\n\nTo reject an invalid combination or order of filters, implementations SHOULD consider the Route Rules with this configuration invalid. If all Route Rules in a Route are invalid, the entire Route would be considered invalid. If only a portion of Route Rules are invalid, implementations MUST set the \"PartiallyInvalid\" condition for the Route.\n\nConformance-levels at this level are defined based on the type of filter:\n\n- ALL core filters MUST be supported by all implementations. - Implementers are encouraged to support extended filters. - Implementation-specific custom filters have no API guarantees across\n  implementations.\n\nSpecifying the same filter multiple times is not supported unless explicitly indicated in the filter.\n\nAll filters are expected to be compatible with each other except for the URLRewrite and RequestRedirect filters, which may not be combined. If an implementation can not support other combinations of filters, they must clearly document that limitation. In cases where incompatible or unsupported filters are specified and cause the `Accepted` condition to be set to status `False`, implementations may use the `IncompatibleFilters` reason to specify this configuration error.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteFilter"
                                                             }
                                                         },
                                                         "matches": {
                                                             "description": "Matches define conditions used for matching the rule against incoming HTTP requests. Each match is independent, i.e. this rule will be matched if **any** one of the matches is satisfied.\n\nFor example, take the following matches configuration:\n\n``` matches: - path:\n    value: \"/foo\"\n  headers:\n  - name: \"version\"\n    value: \"v2\"\n- path:\n    value: \"/v2/foo\"\n```\n\nFor a request to match against this rule, a request must satisfy EITHER of the two conditions:\n\n- path prefixed with `/foo` AND contains the header `version: v2` - path prefix of `/v2/foo`\n\nSee the documentation for HTTPRouteMatch on how to specify multiple match conditions that should be ANDed together.\n\nIf no matches are specified, the default is a prefix path match on \"/\", which has the effect of matching every HTTP request.\n\nProxy or Load Balancer routing configuration generated from HTTPRoutes MUST prioritize matches based on the following criteria, continuing on ties. Across all rules specified on applicable Routes, precedence must be given to the match having:\n\n* \"Exact\" path match. * \"Prefix\" path match with largest number of characters. * Method match. * Largest number of header matches. * Largest number of query param matches.\n\nNote: The precedence of RegularExpression path matches are implementation-specific.\n\nIf ties still exist across multiple Routes, matching precedence MUST be determined in order of the following criteria, continuing on ties:\n\n* The oldest Route based on creation timestamp. * The Route appearing first in alphabetical order by\n  \"{namespace}/{name}\".\n\nIf ties still exist within an HTTPRoute, matching precedence MUST be granted to the FIRST matching rule (in list order) with a match meeting the above criteria.\n\nWhen no rules matching a request have been successfully attached to the parent a request is coming from, a HTTP 404 status code MUST be returned.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteMatch"
                                                             }
                                                         },
                                                         "sessionPersistence": {
                                                             "description": "SessionPersistence defines and configures session persistence for the route rule.\n\nSupport: Extended\n\n\u003cgateway:experimental\u003e",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.SessionPersistence"
                                                         },
                                                         "timeouts": {
                                                             "description": "Timeouts defines the timeouts that can be configured for an HTTP request.\n\nSupport: Extended\n\n\u003cgateway:experimental\u003e",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteTimeouts"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteSpec": {
                                                     "description": "HTTPRouteSpec defines the desired state of HTTPRoute",
                                                     "type": "object",
                                                     "properties": {
                                                         "hostnames": {
                                                             "description": "Hostnames defines a set of hostnames that should match against the HTTP Host header to select a HTTPRoute used to process the request. Implementations MUST ignore any port value specified in the HTTP Host header while performing a match and (absent of any applicable header modification configuration) MUST forward this header unmodified to the backend.\n\nValid values for Hostnames are determined by RFC 1123 definition of a hostname with 2 notable exceptions:\n\n1. IPs are not allowed. 2. A hostname may be prefixed with a wildcard label (`*.`). The wildcard\n   label must appear by itself as the first label.\n\nIf a hostname is specified by both the Listener and HTTPRoute, there must be at least one intersecting hostname for the HTTPRoute to be attached to the Listener. For example:\n\n* A Listener with `test.example.com` as the hostname matches HTTPRoutes\n  that have either not specified any hostnames, or have specified at\n  least one of `test.example.com` or `*.example.com`.\n* A Listener with `*.example.com` as the hostname matches HTTPRoutes\n  that have either not specified any hostnames or have specified at least\n  one hostname that matches the Listener hostname. For example,\n  `*.example.com`, `test.example.com`, and `foo.test.example.com` would\n  all match. On the other hand, `example.com` and `test.example.net` would\n  not match.\n\nHostnames that are prefixed with a wildcard label (`*.`) are interpreted as a suffix match. That means that a match for `*.example.com` would match both `test.example.com`, and `foo.test.example.com`, but not `example.com`.\n\nIf both the Listener and HTTPRoute have specified hostnames, any HTTPRoute hostnames that do not match the Listener hostname MUST be ignored. For example, if a Listener specified `*.example.com`, and the HTTPRoute specified `test.example.com` and `test.example.net`, `test.example.net` must not be considered for a match.\n\nIf both the Listener and HTTPRoute have specified hostnames, and none match with the criteria above, then the HTTPRoute is not accepted. The implementation must raise an 'Accepted' Condition with a status of `False` in the corresponding RouteParentStatus.\n\nIn the event that multiple HTTPRoutes specify intersecting hostnames (e.g. overlapping wildcard matching and exact matching hostnames), precedence must be given to rules from the HTTPRoute with the largest number of:\n\n* Characters in a matching non-wildcard hostname. * Characters in a matching hostname.\n\nIf ties exist across multiple Routes, the matching precedence rules for HTTPRouteMatches takes over.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "parentRefs": {
                                                             "description": "ParentRefs references the resources (usually Gateways) that a Route wants to be attached to. Note that the referenced parent resource needs to allow this for the attachment to be complete. For Gateways, that means the Gateway needs to allow attachment from Routes of this kind and namespace. For Services, that means the Service must either be in the same namespace for a \"producer\" route, or the mesh implementation must support and allow \"consumer\" routes for the referenced Service. ReferenceGrant is not applicable for governing ParentRefs to Services - it is not possible to create a \"producer\" route for a Service in a different namespace from the Route.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nThis API may be extended in the future to support additional kinds of parent resources.\n\nParentRefs must be _distinct_. This means either that:\n\n* They select different objects.  If this is the case, then parentRef\n  entries are distinct. In terms of fields, this means that the\n  multi-part key defined by `group`, `kind`, `namespace`, and `name` must\n  be unique across all parentRef entries in the Route.\n* They do not select different objects, but for each optional field used,\n  each ParentRef that selects the same object must set the same set of\n  optional fields to different values. If one ParentRef sets a\n  combination of optional fields, all must set the same combination.\n\nSome examples:\n\n* If one ParentRef sets `sectionName`, all ParentRefs referencing the\n  same object must also set `sectionName`.\n* If one ParentRef sets `port`, all ParentRefs referencing the same\n  object must also set `port`.\n* If one ParentRef sets `sectionName` and `port`, all ParentRefs\n  referencing the same object must also set `sectionName` and `port`.\n\nIt is possible to separately reference multiple distinct objects that may be collapsed by an implementation. For example, some implementations may choose to merge compatible Gateway Listeners together. If that is the case, the list of routes attached to those resources should also be merged.\n\nNote that for ParentRefs that cross namespace boundaries, there are specific rules. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example, Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable other kinds of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\n\u003cgateway:standard:validation:XValidation:message=\"sectionName must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '')) : true))\"\u003e \u003cgateway:standard:validation:XValidation:message=\"sectionName must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || (has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName))))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__)) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '') \u0026\u0026 (!has(p1.port) || p1.port == 0) == (!has(p2.port) || p2.port == 0)): true))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || ( has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName)) \u0026\u0026 (((!has(p1.port) || p1.port == 0) \u0026\u0026 (!has(p2.port) || p2.port == 0)) || (has(p1.port) \u0026\u0026 has(p2.port) \u0026\u0026 p1.port == p2.port))))\"\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                             }
                                                         },
                                                         "rules": {
                                                             "description": "Rules are a list of HTTP matchers, filters and actions.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteRule"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteStatus": {
                                                     "description": "HTTPRouteStatus defines the observed state of HTTPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "parents"
                                                     ],
                                                     "properties": {
                                                         "parents": {
                                                             "description": "Parents is a list of parent resources (usually Gateways) that are associated with the route, and the status of the route with respect to each parent. When this route attaches to a parent, the controller that manages the parent must add an entry to this list when the controller first sees the route and should update the entry as appropriate when the route or gateway is modified.\n\nNote that parent references that cannot be resolved by an implementation of this API will not be added to this list. Implementations of this API can only populate Route status for the Gateways/parent resources they are responsible for.\n\nA maximum of 32 Gateways will be represented in this list. An empty list means the route has not been attached to any Gateway.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPRouteTimeouts": {
                                                     "description": "HTTPRouteTimeouts defines timeouts that can be configured for an HTTPRoute.",
                                                     "type": "object",
                                                     "properties": {
                                                         "backendRequest": {
                                                             "description": "BackendRequest specifies a timeout for an individual request from the gateway to a backend. This covers the time from when the request first starts being sent from the gateway to when the full response has been received from the backend.\n\nSetting a timeout to the zero duration (e.g. \"0s\") SHOULD disable the timeout completely. Implementations that cannot completely disable the timeout MUST instead interpret the zero duration as the longest possible value to which the timeout can be set.\n\nAn entire client HTTP transaction with a gateway, covered by the Request timeout, may result in more than one call from the gateway to the destination backend, for example, if automatic retries are supported.\n\nBecause the Request timeout encompasses the BackendRequest timeout, the value of BackendRequest must be \u003c= the value of Request timeout.\n\nSupport: Extended",
                                                             "type": "string"
                                                         },
                                                         "request": {
                                                             "description": "Request specifies the maximum duration for a gateway to respond to an HTTP request. If the gateway has not been able to respond before this deadline is met, the gateway MUST return a timeout error.\n\nFor example, setting the `rules.timeouts.request` field to the value `10s` in an `HTTPRoute` will cause a timeout if a client request is taking longer than 10 seconds to complete.\n\nSetting a timeout to the zero duration (e.g. \"0s\") SHOULD disable the timeout completely. Implementations that cannot completely disable the timeout MUST instead interpret the zero duration as the longest possible value to which the timeout can be set.\n\nThis timeout is intended to cover as close to the whole request-response transaction as possible although an implementation MAY choose to start the timeout after the entire request stream has been received instead of immediately after the transaction is initiated by the client.\n\nWhen this field is unspecified, request timeout behavior is implementation-specific.\n\nSupport: Extended",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.HTTPURLRewriteFilter": {
                                                     "description": "HTTPURLRewriteFilter defines a filter that modifies a request during forwarding. At most one of these filters may be used on a Route rule. This MUST NOT be used on the same Route rule as a HTTPRequestRedirect filter.\n\nSupport: Extended\n\n\u003cgateway:experimental\u003e",
                                                     "type": "object",
                                                     "properties": {
                                                         "hostname": {
                                                             "description": "Hostname is the value to be used to replace the Host header value during forwarding.\n\nSupport: Extended",
                                                             "type": "string"
                                                         },
                                                         "path": {
                                                             "description": "Path defines a path rewrite.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPPathModifier"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.Listener": {
                                                     "description": "Listener embodies the concept of a logical endpoint where a Gateway accepts network connections.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "port",
                                                         "protocol"
                                                     ],
                                                     "properties": {
                                                         "allowedRoutes": {
                                                             "description": "AllowedRoutes defines the types of routes that MAY be attached to a Listener and the trusted namespaces where those Route resources MAY be present.\n\nAlthough a client request may match multiple route rules, only one rule may ultimately receive the request. Matching precedence MUST be determined in order of the following criteria:\n\n* The most specific match as defined by the Route type. * The oldest Route based on creation timestamp. For example, a Route with\n  a creation timestamp of \"2020-09-08 01:02:03\" is given precedence over\n  a Route with a creation timestamp of \"2020-09-08 01:02:04\".\n* If everything else is equivalent, the Route appearing first in\n  alphabetical order (namespace/name) should be given precedence. For\n  example, foo/bar is given precedence over foo/baz.\n\nAll valid rules within a Route attached to this Listener should be implemented. Invalid Route rules can be ignored (sometimes that will mean the full Route). If a Route rule transitions from valid to invalid, support for that Route rule should be dropped to ensure consistency. For example, even if a filter specified by a Route rule is invalid, the rest of the rules within that Route should still be supported.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.AllowedRoutes"
                                                         },
                                                         "hostname": {
                                                             "description": "Hostname specifies the virtual hostname to match for protocol types that define this concept. When unspecified, all hostnames are matched. This field is ignored for protocols that don't require hostname based matching.\n\nImplementations MUST apply Hostname matching appropriately for each of the following protocols:\n\n* TLS: The Listener Hostname MUST match the SNI. * HTTP: The Listener Hostname MUST match the Host header of the request. * HTTPS: The Listener Hostname SHOULD match at both the TLS and HTTP\n  protocol layers as described above. If an implementation does not\n  ensure that both the SNI and Host header match the Listener hostname,\n  it MUST clearly document that.\n\nFor HTTPRoute and TLSRoute resources, there is an interaction with the `spec.hostnames` array. When both listener and route specify hostnames, there MUST be an intersection between the values for a Route to be accepted. For more information, refer to the Route specific Hostnames documentation.\n\nHostnames that are prefixed with a wildcard label (`*.`) are interpreted as a suffix match. That means that a match for `*.example.com` would match both `test.example.com`, and `foo.test.example.com`, but not `example.com`.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the Listener. This name MUST be unique within a Gateway.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "port": {
                                                             "description": "Port is the network port. Multiple listeners may use the same port, subject to the Listener compatibility rules.\n\nSupport: Core",
                                                             "type": "integer",
                                                             "format": "int32",
                                                             "default": 0
                                                         },
                                                         "protocol": {
                                                             "description": "Protocol specifies the network protocol this listener expects to receive.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "tls": {
                                                             "description": "TLS is the TLS configuration for the Listener. This field is required if the Protocol field is \"HTTPS\" or \"TLS\". It is invalid to set this field if the Protocol field is \"HTTP\", \"TCP\", or \"UDP\".\n\nThe association of SNIs to Certificate defined in GatewayTLSConfig is defined based on the Hostname field for this listener.\n\nThe GatewayClass MUST use the longest matching SNI out of all available certificates for any TLS handshake.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayTLSConfig"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.ListenerStatus": {
                                                     "description": "ListenerStatus is the status associated with a Listener.",
                                                     "type": "object",
                                                     "required": [
                                                         "name",
                                                         "supportedKinds",
                                                         "attachedRoutes",
                                                         "conditions"
                                                     ],
                                                     "properties": {
                                                         "attachedRoutes": {
                                                             "description": "AttachedRoutes represents the total number of Routes that have been successfully attached to this Listener.\n\nSuccessful attachment of a Route to a Listener is based solely on the combination of the AllowedRoutes field on the corresponding Listener and the Route's ParentRefs field. A Route is successfully attached to a Listener when it is selected by the Listener's AllowedRoutes field AND the Route has a valid ParentRef selecting the whole Gateway resource or a specific Listener as a parent resource (more detail on attachment semantics can be found in the documentation on the various Route kinds ParentRefs fields). Listener or Route status does not impact successful attachment, i.e. the AttachedRoutes field count MUST be set for Listeners with condition Accepted: false and MUST count successfully attached Routes that may themselves have Accepted: false conditions.\n\nUses for this field include troubleshooting Route attachment and measuring blast radius/impact of changes to a Listener.",
                                                             "type": "integer",
                                                             "format": "int32",
                                                             "default": 0
                                                         },
                                                         "conditions": {
                                                             "description": "Conditions describe the current condition of this listener.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Condition"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "type"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the Listener that this status corresponds to.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "supportedKinds": {
                                                             "description": "SupportedKinds is the list indicating the Kinds supported by this listener. This MUST represent the kinds an implementation supports for that Listener configuration.\n\nIf kinds are specified in Spec that are not supported, they MUST NOT appear in this list and an implementation MUST set the \"ResolvedRefs\" condition to \"False\" with the \"InvalidRouteKinds\" reason. If both valid and invalid Route kinds are specified, the implementation MUST reference the valid Route kinds that have been specified.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteGroupKind"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.LocalObjectReference": {
                                                     "description": "LocalObjectReference identifies an API object within the namespace of the referrer. The API object must be valid in the cluster; the Group and Kind must be registered in the cluster for this reference to be valid.\n\nReferences to objects with invalid Group and Kind are not valid, and must be rejected by the implementation, with appropriate Conditions set on the containing object.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the referent. For example \"HTTPRoute\" or \"Service\".",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.LocalParametersReference": {
                                                     "description": "LocalParametersReference identifies an API object containing controller-specific configuration resource within the namespace.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.ObjectReference": {
                                                     "description": "ObjectReference identifies an API object including its namespace.\n\nThe API object must be valid in the cluster; the Group and Kind must be registered in the cluster for this reference to be valid.\n\nReferences to objects with invalid Group and Kind are not valid, and must be rejected by the implementation, with appropriate Conditions set on the containing object.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the referent. For example \"ConfigMap\" or \"Service\".",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the referenced object. When unspecified, the local namespace is inferred.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nSupport: Core",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.ParametersReference": {
                                                     "description": "ParametersReference identifies an API object containing controller-specific configuration resource within the cluster.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the referent. This field is required when referring to a Namespace-scoped resource and MUST be unset when referring to a Cluster-scoped resource.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.ParentReference": {
                                                     "description": "ParentReference identifies an API object (usually a Gateway) that can be considered a parent of this resource (usually a route). The only kind of parent resource with \"Core\" support is Gateway. This API may be extended in the future to support additional kinds of parent resources, such as HTTPRoute.\n\nNote that there are specific rules for ParentRefs which cross namespace boundaries. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example: Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable any other kind of cross-namespace reference.\n\nThe API object must be valid in the cluster; the Group and Kind must be registered in the cluster for this reference to be valid.",
                                                     "type": "object",
                                                     "required": [
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. When unspecified, \"gateway.networking.k8s.io\" is inferred. To set the core API group (such as for a \"Service\" kind referent), Group must be explicitly set to \"\" (empty string).\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the referent.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nSupport for other resources is Implementation-Specific.",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the referent. When unspecified, this refers to the local namespace of the Route.\n\nNote that there are specific rules for ParentRefs which cross namespace boundaries. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example: Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable any other kind of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "port": {
                                                             "description": "Port is the network port this Route targets. It can be interpreted differently based on the type of parent resource.\n\nWhen the parent resource is a Gateway, this targets all listeners listening on the specified port that also support this kind of Route(and select this Route). It's not recommended to set `Port` unless the networking behaviors specified in a Route must apply to a specific port as opposed to a listener(s) whose port(s) may be changed. When both Port and SectionName are specified, the name and port of the selected listener must match both specified values.\n\n\u003cgateway:experimental:description\u003e When the parent resource is a Service, this targets a specific port in the Service spec. When both Port (experimental) and SectionName are specified, the name and port of the selected port must match both specified values. \u003c/gateway:experimental:description\u003e\n\nImplementations MAY choose to support other parent resources. Implementations supporting other types of parent resources MUST clearly document how/if Port is interpreted.\n\nFor the purpose of status, an attachment is considered successful as long as the parent resource accepts it partially. For example, Gateway listeners can restrict which Routes can attach to them by Route kind, namespace, or hostname. If 1 of 2 Gateway listeners accept attachment from the referencing Route, the Route MUST be considered successfully attached. If no Gateway listeners accept attachment from this Route, the Route MUST be considered detached from the Gateway.\n\nSupport: Extended",
                                                             "type": "integer",
                                                             "format": "int32"
                                                         },
                                                         "sectionName": {
                                                             "description": "SectionName is the name of a section within the target resource. In the following resources, SectionName is interpreted as the following:\n\n* Gateway: Listener name. When both Port (experimental) and SectionName are specified, the name and port of the selected listener must match both specified values. * Service: Port name. When both Port (experimental) and SectionName are specified, the name and port of the selected listener must match both specified values.\n\nImplementations MAY choose to support attaching Routes to other resources. If that is the case, they MUST clearly document how SectionName is interpreted.\n\nWhen unspecified (empty string), this will reference the entire resource. For the purpose of status, an attachment is considered successful if at least one section in the parent resource accepts it. For example, Gateway listeners can restrict which Routes can attach to them by Route kind, namespace, or hostname. If 1 of 2 Gateway listeners accept attachment from the referencing Route, the Route MUST be considered successfully attached. If no Gateway listeners accept attachment from this Route, the Route MUST be considered detached from the Gateway.\n\nSupport: Core",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.RouteGroupKind": {
                                                     "description": "RouteGroupKind indicates the group and kind of a Route resource.",
                                                     "type": "object",
                                                     "required": [
                                                         "kind"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the Route.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the kind of the Route.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.RouteNamespaces": {
                                                     "description": "RouteNamespaces indicate which namespaces Routes should be selected from.",
                                                     "type": "object",
                                                     "properties": {
                                                         "from": {
                                                             "description": "From indicates where Routes will be selected for this Gateway. Possible values are:\n\n* All: Routes in all namespaces may be used by this Gateway. * Selector: Routes in namespaces selected by the selector may be used by\n  this Gateway.\n* Same: Only Routes in the same namespace may be used by this Gateway.\n\nSupport: Core",
                                                             "type": "string"
                                                         },
                                                         "selector": {
                                                             "description": "Selector must be specified when From is set to \"Selector\". In that case, only Routes in Namespaces matching this Selector will be selected by this Gateway. This field is ignored for other values of \"From\".\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.LabelSelector"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus": {
                                                     "description": "RouteParentStatus describes the status of a route with respect to an associated Parent.",
                                                     "type": "object",
                                                     "required": [
                                                         "parentRef",
                                                         "controllerName"
                                                     ],
                                                     "properties": {
                                                         "conditions": {
                                                             "description": "Conditions describes the status of the route with respect to the Gateway. Note that the route's availability is also subject to the Gateway's own status conditions and listener status.\n\nIf the Route's ParentRef specifies an existing Gateway that supports Routes of this kind AND that Gateway's controller has sufficient access, then that Gateway's controller MUST set the \"Accepted\" condition on the Route, to indicate whether the route has been accepted or rejected by the Gateway, and why.\n\nA Route MUST be considered \"Accepted\" if at least one of the Route's rules is implemented by the Gateway.\n\nThere are a number of cases where the \"Accepted\" condition may not be set due to lack of controller visibility, that includes when:\n\n* The Route refers to a non-existent parent. * The Route is of a type that the controller does not support. * The Route is in a namespace the controller does not have access to.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Condition"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "type"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "controllerName": {
                                                             "description": "ControllerName is a domain/path string that indicates the name of the controller that wrote this status. This corresponds with the controllerName field on GatewayClass.\n\nExample: \"example.net/gateway-controller\".\n\nThe format of this field is DOMAIN \"/\" PATH, where DOMAIN and PATH are valid Kubernetes names (https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names).\n\nControllers MUST populate this field when writing status. Controllers should ensure that entries to status populated with their ControllerName are cleaned up when they are no longer necessary.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "parentRef": {
                                                             "description": "ParentRef corresponds with a ParentRef in the spec that this RouteParentStatus struct describes the status of.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.RouteStatus": {
                                                     "description": "RouteStatus defines the common attributes that all Routes MUST include within their status.",
                                                     "type": "object",
                                                     "required": [
                                                         "parents"
                                                     ],
                                                     "properties": {
                                                         "parents": {
                                                             "description": "Parents is a list of parent resources (usually Gateways) that are associated with the route, and the status of the route with respect to each parent. When this route attaches to a parent, the controller that manages the parent must add an entry to this list when the controller first sees the route and should update the entry as appropriate when the route or gateway is modified.\n\nNote that parent references that cannot be resolved by an implementation of this API will not be added to this list. Implementations of this API can only populate Route status for the Gateways/parent resources they are responsible for.\n\nA maximum of 32 Gateways will be represented in this list. An empty list means the route has not been attached to any Gateway.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.SecretObjectReference": {
                                                     "description": "SecretObjectReference identifies an API object including its namespace, defaulting to Secret.\n\nThe API object must be valid in the cluster; the Group and Kind must be registered in the cluster for this reference to be valid.\n\nReferences to objects with invalid Group and Kind are not valid, and must be rejected by the implementation, with appropriate Conditions set on the containing object.",
                                                     "type": "object",
                                                     "required": [
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. For example, \"gateway.networking.k8s.io\". When unspecified or empty string, core API group is inferred.",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the referent. For example \"Secret\".",
                                                             "type": "string"
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the referenced object. When unspecified, the local namespace is inferred.\n\nNote that when a namespace different than the local namespace is specified, a ReferenceGrant object is required in the referent namespace to allow that namespace's owner to accept the reference. See the ReferenceGrant documentation for details.\n\nSupport: Core",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1.SessionPersistence": {
                                                     "description": "SessionPersistence defines the desired state of SessionPersistence.",
                                                     "type": "object",
                                                     "properties": {
                                                         "absoluteTimeout": {
                                                             "description": "AbsoluteTimeout defines the absolute timeout of the persistent session. Once the AbsoluteTimeout duration has elapsed, the session becomes invalid.\n\nSupport: Extended",
                                                             "type": "string"
                                                         },
                                                         "cookieConfig": {
                                                             "description": "CookieConfig provides configuration settings that are specific to cookie-based session persistence.\n\nSupport: Core",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.CookieConfig"
                                                         },
                                                         "idleTimeout": {
                                                             "description": "IdleTimeout defines the idle timeout of the persistent session. Once the session has been idle for more than the specified IdleTimeout duration, the session becomes invalid.\n\nSupport: Extended",
                                                             "type": "string"
                                                         },
                                                         "sessionName": {
                                                             "description": "SessionName defines the name of the persistent session token which may be reflected in the cookie or the header. Users should avoid reusing session names to prevent unintended consequences, such as rejection or unpredictable behavior.\n\nSupport: Implementation-specific",
                                                             "type": "string"
                                                         },
                                                         "type": {
                                                             "description": "Type defines the type of session persistence such as through the use a header or cookie. Defaults to cookie based session persistence.\n\nSupport: Core for \"Cookie\" type\n\nSupport: Extended for \"Header\" type",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.BackendLBPolicy": {
                                                     "description": "BackendLBPolicy provides a way to define load balancing rules for a backend.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of BackendLBPolicy.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.BackendLBPolicySpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of BackendLBPolicy.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.PolicyStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.BackendLBPolicyList": {
                                                     "description": "BackendLBPolicyList contains a list of BackendLBPolicies",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.BackendLBPolicy"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.BackendLBPolicySpec": {
                                                     "description": "BackendLBPolicySpec defines the desired state of BackendLBPolicy. Note: there is no Override or Default policy configuration.",
                                                     "type": "object",
                                                     "required": [
                                                         "targetRefs"
                                                     ],
                                                     "properties": {
                                                         "sessionPersistence": {
                                                             "description": "SessionPersistence defines and configures session persistence for the backend.\n\nSupport: Extended",
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.SessionPersistence"
                                                         },
                                                         "targetRefs": {
                                                             "description": "TargetRef identifies an API object to apply policy to. Currently, Backends (i.e. Service, ServiceImport, or any implementation-specific backendRef) are the only valid API target references.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.LocalPolicyTargetReference"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "group",
                                                                 "kind",
                                                                 "name"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.GRPCRoute": {
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of GRPCRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of GRPCRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GRPCRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.GRPCRouteList": {
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.GRPCRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.LocalPolicyTargetReference": {
                                                     "description": "LocalPolicyTargetReference identifies an API object to apply a direct or inherited policy to. This should be used as part of Policy resources that can target Gateway API resources. For more information on how this policy attachment model works, and a sample Policy resource, refer to the policy attachment documentation for Gateway API.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.LocalPolicyTargetReferenceWithSectionName": {
                                                     "description": "LocalPolicyTargetReferenceWithSectionName identifies an API object to apply a direct policy to. This should be used as part of Policy resources that can target single resources. For more information on how this policy attachment mode works, and a sample Policy resource, refer to the policy attachment documentation for Gateway API.\n\nNote: This should only be used for direct policy attachment when references to SectionName are actually needed. In all other cases, LocalPolicyTargetReference should be used.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "sectionName": {
                                                             "description": "SectionName is the name of a section within the target resource. When unspecified, this targetRef targets the entire resource. In the following resources, SectionName is interpreted as the following:\n\n* Gateway: Listener name * HTTPRoute: HTTPRouteRule name * Service: Port name\n\nIf a SectionName is specified, but does not exist on the targeted object, the Policy must fail to attach, and the policy implementation should record a `ResolvedRefs` or similar Condition in the Policy's status.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.NamespacedPolicyTargetReference": {
                                                     "description": "NamespacedPolicyTargetReference identifies an API object to apply a direct or inherited policy to, potentially in a different namespace. This should only be used as part of Policy resources that need to be able to target resources in different namespaces. For more information on how this policy attachment model works, and a sample Policy resource, refer to the policy attachment documentation for Gateway API.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "name"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is kind of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the target resource.",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the referent. When unspecified, the local namespace is inferred. Even when policy targets a resource in a different namespace, it MUST only apply to traffic originating from the same namespace as the policy.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.PolicyAncestorStatus": {
                                                     "description": "PolicyAncestorStatus describes the status of a route with respect to an associated Ancestor.\n\nAncestors refer to objects that are either the Target of a policy or above it in terms of object hierarchy. For example, if a policy targets a Service, the Policy's Ancestors are, in order, the Service, the HTTPRoute, the Gateway, and the GatewayClass. Almost always, in this hierarchy, the Gateway will be the most useful object to place Policy status on, so we recommend that implementations SHOULD use Gateway as the PolicyAncestorStatus object unless the designers have a _very_ good reason otherwise.\n\nIn the context of policy attachment, the Ancestor is used to distinguish which resource results in a distinct application of this policy. For example, if a policy targets a Service, it may have a distinct result per attached Gateway.\n\nPolicies targeting the same resource may have different effects depending on the ancestors of those resources. For example, different Gateways targeting the same Service may have different capabilities, especially if they have different underlying implementations.\n\nFor example, in BackendTLSPolicy, the Policy attaches to a Service that is used as a backend in a HTTPRoute that is itself attached to a Gateway. In this case, the relevant object for status is the Gateway, and that is the ancestor object referred to in this status.\n\nNote that a parent is also an ancestor, so for objects where the parent is the relevant object for status, this struct SHOULD still be used.\n\nThis struct is intended to be used in a slice that's effectively a map, with a composite key made up of the AncestorRef and the ControllerName.",
                                                     "type": "object",
                                                     "required": [
                                                         "ancestorRef",
                                                         "controllerName"
                                                     ],
                                                     "properties": {
                                                         "ancestorRef": {
                                                             "description": "AncestorRef corresponds with a ParentRef in the spec that this PolicyAncestorStatus struct describes the status of.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                         },
                                                         "conditions": {
                                                             "description": "Conditions describes the status of the Policy with respect to the given Ancestor.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.Condition"
                                                             },
                                                             "x-kubernetes-list-map-keys": [
                                                                 "type"
                                                             ],
                                                             "x-kubernetes-list-type": "map"
                                                         },
                                                         "controllerName": {
                                                             "description": "ControllerName is a domain/path string that indicates the name of the controller that wrote this status. This corresponds with the controllerName field on GatewayClass.\n\nExample: \"example.net/gateway-controller\".\n\nThe format of this field is DOMAIN \"/\" PATH, where DOMAIN and PATH are valid Kubernetes names (https://kubernetes.io/docs/concepts/overview/working-with-objects/names/#names).\n\nControllers MUST populate this field when writing status. Controllers should ensure that entries to status populated with their ControllerName are cleaned up when they are no longer necessary.",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.PolicyStatus": {
                                                     "description": "PolicyStatus defines the common attributes that all Policies should include within their status.",
                                                     "type": "object",
                                                     "required": [
                                                         "ancestors"
                                                     ],
                                                     "properties": {
                                                         "ancestors": {
                                                             "description": "Ancestors is a list of ancestor resources (usually Gateways) that are associated with the policy, and the status of the policy with respect to each ancestor. When this policy attaches to a parent, the controller that manages the parent and the ancestors MUST add an entry to this list when the controller first sees the policy and SHOULD update the entry as appropriate when the relevant ancestor is modified.\n\nNote that choosing the relevant ancestor is left to the Policy designers; an important part of Policy design is designing the right object level at which to namespace this status.\n\nNote also that implementations MUST ONLY populate ancestor status for the Ancestor resources they are responsible for. Implementations MUST use the ControllerName field to uniquely identify the entries in this list that they are responsible for.\n\nNote that to achieve this, the list of PolicyAncestorStatus structs MUST be treated as a map with a composite key, made up of the AncestorRef and ControllerName fields combined.\n\nA maximum of 16 ancestors will be represented in this list. An empty list means the Policy is not relevant for any ancestors.\n\nIf this slice is full, implementations MUST NOT add further entries. Instead they MUST consider the policy unimplementable and signal that on any related resources such as the ancestor that would be referenced here. For example, if this list was full on BackendTLSPolicy, no additional Gateways would be able to reference the Service targeted by the BackendTLSPolicy.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.PolicyAncestorStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.ReferenceGrant": {
                                                     "description": "ReferenceGrant identifies kinds of resources in other namespaces that are trusted to reference the specified kinds of resources in the same namespace as the policy.\n\nEach ReferenceGrant can be used to represent a unique trust relationship. Additional Reference Grants can be used to add to the set of trusted sources of inbound references for the namespace they are defined within.\n\nA ReferenceGrant is required for all cross-namespace references in Gateway API (with the exception of cross-namespace Route-Gateway attachment, which is governed by the AllowedRoutes configuration on the Gateway, and cross-namespace Service ParentRefs on a \"consumer\" mesh Route, which defines routing rules applicable only to workloads in the Route namespace). ReferenceGrants allowing a reference from a Route to a Service are only applicable to BackendRefs.\n\nReferenceGrant is a form of runtime verification allowing users to assert which cross-namespace object references are permitted. Implementations that support ReferenceGrant MUST NOT permit cross-namespace references which have no grant, and MUST respond to the removal of a grant by revoking the access that the grant allowed.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of ReferenceGrant.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantSpec"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.ReferenceGrantList": {
                                                     "description": "ReferenceGrantList contains a list of ReferenceGrant.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.ReferenceGrant"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRoute": {
                                                     "description": "TCPRoute provides a way to route TCP requests. When combined with a Gateway listener, it can be used to forward connections on the port specified by the listener to a set of backends specified by the TCPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of TCPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of TCPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteList": {
                                                     "description": "TCPRouteList contains a list of TCPRoute",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteRule": {
                                                     "description": "TCPRouteRule is the configuration for a given rule.",
                                                     "type": "object",
                                                     "properties": {
                                                         "backendRefs": {
                                                             "description": "BackendRefs defines the backend(s) where matching requests should be sent. If unspecified or invalid (refers to a non-existent resource or a Service with no endpoints), the underlying implementation MUST actively reject connection attempts to this backend. Connection rejections must respect weight; if an invalid backend is requested to have 80% of connections, then 80% of connections must be rejected instead.\n\nSupport: Core for Kubernetes Service\n\nSupport: Extended for Kubernetes ServiceImport\n\nSupport: Implementation-specific for any other resource\n\nSupport for weight: Extended",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.BackendRef"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteSpec": {
                                                     "description": "TCPRouteSpec defines the desired state of TCPRoute",
                                                     "type": "object",
                                                     "required": [
                                                         "rules"
                                                     ],
                                                     "properties": {
                                                         "parentRefs": {
                                                             "description": "ParentRefs references the resources (usually Gateways) that a Route wants to be attached to. Note that the referenced parent resource needs to allow this for the attachment to be complete. For Gateways, that means the Gateway needs to allow attachment from Routes of this kind and namespace. For Services, that means the Service must either be in the same namespace for a \"producer\" route, or the mesh implementation must support and allow \"consumer\" routes for the referenced Service. ReferenceGrant is not applicable for governing ParentRefs to Services - it is not possible to create a \"producer\" route for a Service in a different namespace from the Route.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nThis API may be extended in the future to support additional kinds of parent resources.\n\nParentRefs must be _distinct_. This means either that:\n\n* They select different objects.  If this is the case, then parentRef\n  entries are distinct. In terms of fields, this means that the\n  multi-part key defined by `group`, `kind`, `namespace`, and `name` must\n  be unique across all parentRef entries in the Route.\n* They do not select different objects, but for each optional field used,\n  each ParentRef that selects the same object must set the same set of\n  optional fields to different values. If one ParentRef sets a\n  combination of optional fields, all must set the same combination.\n\nSome examples:\n\n* If one ParentRef sets `sectionName`, all ParentRefs referencing the\n  same object must also set `sectionName`.\n* If one ParentRef sets `port`, all ParentRefs referencing the same\n  object must also set `port`.\n* If one ParentRef sets `sectionName` and `port`, all ParentRefs\n  referencing the same object must also set `sectionName` and `port`.\n\nIt is possible to separately reference multiple distinct objects that may be collapsed by an implementation. For example, some implementations may choose to merge compatible Gateway Listeners together. If that is the case, the list of routes attached to those resources should also be merged.\n\nNote that for ParentRefs that cross namespace boundaries, there are specific rules. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example, Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable other kinds of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\n\u003cgateway:standard:validation:XValidation:message=\"sectionName must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '')) : true))\"\u003e \u003cgateway:standard:validation:XValidation:message=\"sectionName must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || (has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName))))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__)) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '') \u0026\u0026 (!has(p1.port) || p1.port == 0) == (!has(p2.port) || p2.port == 0)): true))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || ( has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName)) \u0026\u0026 (((!has(p1.port) || p1.port == 0) \u0026\u0026 (!has(p2.port) || p2.port == 0)) || (has(p1.port) \u0026\u0026 has(p2.port) \u0026\u0026 p1.port == p2.port))))\"\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                             }
                                                         },
                                                         "rules": {
                                                             "description": "Rules are a list of TCP matchers and actions.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteRule"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TCPRouteStatus": {
                                                     "description": "TCPRouteStatus defines the observed state of TCPRoute",
                                                     "type": "object",
                                                     "required": [
                                                         "parents"
                                                     ],
                                                     "properties": {
                                                         "parents": {
                                                             "description": "Parents is a list of parent resources (usually Gateways) that are associated with the route, and the status of the route with respect to each parent. When this route attaches to a parent, the controller that manages the parent must add an entry to this list when the controller first sees the route and should update the entry as appropriate when the route or gateway is modified.\n\nNote that parent references that cannot be resolved by an implementation of this API will not be added to this list. Implementations of this API can only populate Route status for the Gateways/parent resources they are responsible for.\n\nA maximum of 32 Gateways will be represented in this list. An empty list means the route has not been attached to any Gateway.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRoute": {
                                                     "description": "The TLSRoute resource is similar to TCPRoute, but can be configured to match against TLS-specific metadata. This allows more flexibility in matching streams for a given TLS listener.\n\nIf you need to forward traffic to a single target for a TLS listener, you could choose to use a TCPRoute with a TLS listener.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of TLSRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of TLSRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteList": {
                                                     "description": "TLSRouteList contains a list of TLSRoute",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteRule": {
                                                     "description": "TLSRouteRule is the configuration for a given rule.",
                                                     "type": "object",
                                                     "properties": {
                                                         "backendRefs": {
                                                             "description": "BackendRefs defines the backend(s) where matching requests should be sent. If unspecified or invalid (refers to a non-existent resource or a Service with no endpoints), the rule performs no forwarding; if no filters are specified that would result in a response being sent, the underlying implementation must actively reject request attempts to this backend, by rejecting the connection or returning a 500 status code. Request rejections must respect weight; if an invalid backend is requested to have 80% of requests, then 80% of requests must be rejected instead.\n\nSupport: Core for Kubernetes Service\n\nSupport: Extended for Kubernetes ServiceImport\n\nSupport: Implementation-specific for any other resource\n\nSupport for weight: Extended",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.BackendRef"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteSpec": {
                                                     "description": "TLSRouteSpec defines the desired state of a TLSRoute resource.",
                                                     "type": "object",
                                                     "required": [
                                                         "rules"
                                                     ],
                                                     "properties": {
                                                         "hostnames": {
                                                             "description": "Hostnames defines a set of SNI names that should match against the SNI attribute of TLS ClientHello message in TLS handshake. This matches the RFC 1123 definition of a hostname with 2 notable exceptions:\n\n1. IPs are not allowed in SNI names per RFC 6066. 2. A hostname may be prefixed with a wildcard label (`*.`). The wildcard\n   label must appear by itself as the first label.\n\nIf a hostname is specified by both the Listener and TLSRoute, there must be at least one intersecting hostname for the TLSRoute to be attached to the Listener. For example:\n\n* A Listener with `test.example.com` as the hostname matches TLSRoutes\n  that have either not specified any hostnames, or have specified at\n  least one of `test.example.com` or `*.example.com`.\n* A Listener with `*.example.com` as the hostname matches TLSRoutes\n  that have either not specified any hostnames or have specified at least\n  one hostname that matches the Listener hostname. For example,\n  `test.example.com` and `*.example.com` would both match. On the other\n  hand, `example.com` and `test.example.net` would not match.\n\nIf both the Listener and TLSRoute have specified hostnames, any TLSRoute hostnames that do not match the Listener hostname MUST be ignored. For example, if a Listener specified `*.example.com`, and the TLSRoute specified `test.example.com` and `test.example.net`, `test.example.net` must not be considered for a match.\n\nIf both the Listener and TLSRoute have specified hostnames, and none match with the criteria above, then the TLSRoute is not accepted. The implementation must raise an 'Accepted' Condition with a status of `False` in the corresponding RouteParentStatus.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "type": "string",
                                                                 "default": ""
                                                             }
                                                         },
                                                         "parentRefs": {
                                                             "description": "ParentRefs references the resources (usually Gateways) that a Route wants to be attached to. Note that the referenced parent resource needs to allow this for the attachment to be complete. For Gateways, that means the Gateway needs to allow attachment from Routes of this kind and namespace. For Services, that means the Service must either be in the same namespace for a \"producer\" route, or the mesh implementation must support and allow \"consumer\" routes for the referenced Service. ReferenceGrant is not applicable for governing ParentRefs to Services - it is not possible to create a \"producer\" route for a Service in a different namespace from the Route.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nThis API may be extended in the future to support additional kinds of parent resources.\n\nParentRefs must be _distinct_. This means either that:\n\n* They select different objects.  If this is the case, then parentRef\n  entries are distinct. In terms of fields, this means that the\n  multi-part key defined by `group`, `kind`, `namespace`, and `name` must\n  be unique across all parentRef entries in the Route.\n* They do not select different objects, but for each optional field used,\n  each ParentRef that selects the same object must set the same set of\n  optional fields to different values. If one ParentRef sets a\n  combination of optional fields, all must set the same combination.\n\nSome examples:\n\n* If one ParentRef sets `sectionName`, all ParentRefs referencing the\n  same object must also set `sectionName`.\n* If one ParentRef sets `port`, all ParentRefs referencing the same\n  object must also set `port`.\n* If one ParentRef sets `sectionName` and `port`, all ParentRefs\n  referencing the same object must also set `sectionName` and `port`.\n\nIt is possible to separately reference multiple distinct objects that may be collapsed by an implementation. For example, some implementations may choose to merge compatible Gateway Listeners together. If that is the case, the list of routes attached to those resources should also be merged.\n\nNote that for ParentRefs that cross namespace boundaries, there are specific rules. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example, Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable other kinds of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\n\u003cgateway:standard:validation:XValidation:message=\"sectionName must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '')) : true))\"\u003e \u003cgateway:standard:validation:XValidation:message=\"sectionName must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || (has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName))))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__)) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '') \u0026\u0026 (!has(p1.port) || p1.port == 0) == (!has(p2.port) || p2.port == 0)): true))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || ( has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName)) \u0026\u0026 (((!has(p1.port) || p1.port == 0) \u0026\u0026 (!has(p2.port) || p2.port == 0)) || (has(p1.port) \u0026\u0026 has(p2.port) \u0026\u0026 p1.port == p2.port))))\"\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                             }
                                                         },
                                                         "rules": {
                                                             "description": "Rules are a list of TLS matchers and actions.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteRule"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.TLSRouteStatus": {
                                                     "description": "TLSRouteStatus defines the observed state of TLSRoute",
                                                     "type": "object",
                                                     "required": [
                                                         "parents"
                                                     ],
                                                     "properties": {
                                                         "parents": {
                                                             "description": "Parents is a list of parent resources (usually Gateways) that are associated with the route, and the status of the route with respect to each parent. When this route attaches to a parent, the controller that manages the parent must add an entry to this list when the controller first sees the route and should update the entry as appropriate when the route or gateway is modified.\n\nNote that parent references that cannot be resolved by an implementation of this API will not be added to this list. Implementations of this API can only populate Route status for the Gateways/parent resources they are responsible for.\n\nA maximum of 32 Gateways will be represented in this list. An empty list means the route has not been attached to any Gateway.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRoute": {
                                                     "description": "UDPRoute provides a way to route UDP traffic. When combined with a Gateway listener, it can be used to forward traffic on the port specified by the listener to a set of backends specified by the UDPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of UDPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of UDPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteList": {
                                                     "description": "UDPRouteList contains a list of UDPRoute",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteRule": {
                                                     "description": "UDPRouteRule is the configuration for a given rule.",
                                                     "type": "object",
                                                     "properties": {
                                                         "backendRefs": {
                                                             "description": "BackendRefs defines the backend(s) where matching requests should be sent. If unspecified or invalid (refers to a non-existent resource or a Service with no endpoints), the underlying implementation MUST actively reject connection attempts to this backend. Packet drops must respect weight; if an invalid backend is requested to have 80% of the packets, then 80% of packets must be dropped instead.\n\nSupport: Core for Kubernetes Service\n\nSupport: Extended for Kubernetes ServiceImport\n\nSupport: Implementation-specific for any other resource\n\nSupport for weight: Extended",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.BackendRef"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteSpec": {
                                                     "description": "UDPRouteSpec defines the desired state of UDPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "rules"
                                                     ],
                                                     "properties": {
                                                         "parentRefs": {
                                                             "description": "ParentRefs references the resources (usually Gateways) that a Route wants to be attached to. Note that the referenced parent resource needs to allow this for the attachment to be complete. For Gateways, that means the Gateway needs to allow attachment from Routes of this kind and namespace. For Services, that means the Service must either be in the same namespace for a \"producer\" route, or the mesh implementation must support and allow \"consumer\" routes for the referenced Service. ReferenceGrant is not applicable for governing ParentRefs to Services - it is not possible to create a \"producer\" route for a Service in a different namespace from the Route.\n\nThere are two kinds of parent resources with \"Core\" support:\n\n* Gateway (Gateway conformance profile) * Service (Mesh conformance profile, ClusterIP Services only)\n\nThis API may be extended in the future to support additional kinds of parent resources.\n\nParentRefs must be _distinct_. This means either that:\n\n* They select different objects.  If this is the case, then parentRef\n  entries are distinct. In terms of fields, this means that the\n  multi-part key defined by `group`, `kind`, `namespace`, and `name` must\n  be unique across all parentRef entries in the Route.\n* They do not select different objects, but for each optional field used,\n  each ParentRef that selects the same object must set the same set of\n  optional fields to different values. If one ParentRef sets a\n  combination of optional fields, all must set the same combination.\n\nSome examples:\n\n* If one ParentRef sets `sectionName`, all ParentRefs referencing the\n  same object must also set `sectionName`.\n* If one ParentRef sets `port`, all ParentRefs referencing the same\n  object must also set `port`.\n* If one ParentRef sets `sectionName` and `port`, all ParentRefs\n  referencing the same object must also set `sectionName` and `port`.\n\nIt is possible to separately reference multiple distinct objects that may be collapsed by an implementation. For example, some implementations may choose to merge compatible Gateway Listeners together. If that is the case, the list of routes attached to those resources should also be merged.\n\nNote that for ParentRefs that cross namespace boundaries, there are specific rules. Cross-namespace references are only valid if they are explicitly allowed by something in the namespace they are referring to. For example, Gateway has the AllowedRoutes field, and ReferenceGrant provides a generic way to enable other kinds of cross-namespace reference.\n\n\u003cgateway:experimental:description\u003e ParentRefs from a Route to a Service in the same namespace are \"producer\" routes, which apply default routing rules to inbound connections from any namespace to the Service.\n\nParentRefs from a Route to a Service in a different namespace are \"consumer\" routes, and these routing rules are only applied to outbound connections originating from the same namespace as the Route, for which the intended destination of the connections are a Service targeted as a ParentRef of the Route. \u003c/gateway:experimental:description\u003e\n\n\u003cgateway:standard:validation:XValidation:message=\"sectionName must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '')) : true))\"\u003e \u003cgateway:standard:validation:XValidation:message=\"sectionName must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || (has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName))))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be specified when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.all(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__)) ? ((!has(p1.sectionName) || p1.sectionName == '') == (!has(p2.sectionName) || p2.sectionName == '') \u0026\u0026 (!has(p1.port) || p1.port == 0) == (!has(p2.port) || p2.port == 0)): true))\"\u003e \u003cgateway:experimental:validation:XValidation:message=\"sectionName or port must be unique when parentRefs includes 2 or more references to the same parent\",rule=\"self.all(p1, self.exists_one(p2, p1.group == p2.group \u0026\u0026 p1.kind == p2.kind \u0026\u0026 p1.name == p2.name \u0026\u0026 (((!has(p1.__namespace__) || p1.__namespace__ == '') \u0026\u0026 (!has(p2.__namespace__) || p2.__namespace__ == '')) || (has(p1.__namespace__) \u0026\u0026 has(p2.__namespace__) \u0026\u0026 p1.__namespace__ == p2.__namespace__ )) \u0026\u0026 (((!has(p1.sectionName) || p1.sectionName == '') \u0026\u0026 (!has(p2.sectionName) || p2.sectionName == '')) || ( has(p1.sectionName) \u0026\u0026 has(p2.sectionName) \u0026\u0026 p1.sectionName == p2.sectionName)) \u0026\u0026 (((!has(p1.port) || p1.port == 0) \u0026\u0026 (!has(p2.port) || p2.port == 0)) || (has(p1.port) \u0026\u0026 has(p2.port) \u0026\u0026 p1.port == p2.port))))\"\u003e",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.ParentReference"
                                                             }
                                                         },
                                                         "rules": {
                                                             "description": "Rules are a list of UDP matchers and actions.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteRule"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha2.UDPRouteStatus": {
                                                     "description": "UDPRouteStatus defines the observed state of UDPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "parents"
                                                     ],
                                                     "properties": {
                                                         "parents": {
                                                             "description": "Parents is a list of parent resources (usually Gateways) that are associated with the route, and the status of the route with respect to each parent. When this route attaches to a parent, the controller that manages the parent must add an entry to this list when the controller first sees the route and should update the entry as appropriate when the route or gateway is modified.\n\nNote that parent references that cannot be resolved by an implementation of this API will not be added to this list. Implementations of this API can only populate Route status for the Gateways/parent resources they are responsible for.\n\nA maximum of 32 Gateways will be represented in this list. An empty list means the route has not been attached to any Gateway.",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.RouteParentStatus"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicy": {
                                                     "description": "BackendTLSPolicy provides a way to configure how a Gateway connects to a Backend via TLS.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of BackendTLSPolicy.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicySpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of BackendTLSPolicy.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.PolicyStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicyList": {
                                                     "description": "BackendTLSPolicyList contains a list of BackendTLSPolicies",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicy"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicySpec": {
                                                     "description": "BackendTLSPolicySpec defines the desired state of BackendTLSPolicy.\n\nSupport: Extended",
                                                     "type": "object",
                                                     "required": [
                                                         "targetRefs",
                                                         "validation"
                                                     ],
                                                     "properties": {
                                                         "targetRefs": {
                                                             "description": "TargetRefs identifies an API object to apply the policy to. Only Services have Extended support. Implementations MAY support additional objects, with Implementation Specific support. Note that this config applies to the entire referenced resource by default, but this default may change in the future to provide a more granular application of the policy.\n\nSupport: Extended for Kubernetes Service\n\nSupport: Implementation-specific for any other resource",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha2.LocalPolicyTargetReferenceWithSectionName"
                                                             }
                                                         },
                                                         "validation": {
                                                             "description": "Validation contains backend TLS validation configuration.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicyValidation"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1alpha3.BackendTLSPolicyValidation": {
                                                     "description": "BackendTLSPolicyValidation contains backend TLS validation configuration.",
                                                     "type": "object",
                                                     "required": [
                                                         "hostname"
                                                     ],
                                                     "properties": {
                                                         "caCertificateRefs": {
                                                             "description": "CACertificateRefs contains one or more references to Kubernetes objects that contain a PEM-encoded TLS CA certificate bundle, which is used to validate a TLS handshake between the Gateway and backend Pod.\n\nIf CACertificateRefs is empty or unspecified, then WellKnownCACertificates must be specified. Only one of CACertificateRefs or WellKnownCACertificates may be specified, not both. If CACertifcateRefs is empty or unspecified, the configuration for WellKnownCACertificates MUST be honored instead if supported by the implementation.\n\nReferences to a resource in a different namespace are invalid for the moment, although we will revisit this in the future.\n\nA single CACertificateRef to a Kubernetes ConfigMap kind has \"Core\" support. Implementations MAY choose to support attaching multiple certificates to a backend, but this behavior is implementation-specific.\n\nSupport: Core - An optional single reference to a Kubernetes ConfigMap, with the CA certificate in a key named `ca.crt`.\n\nSupport: Implementation-specific (More than one reference, or other kinds of resources).",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.LocalObjectReference"
                                                             }
                                                         },
                                                         "hostname": {
                                                             "description": "Hostname is used for two purposes in the connection between Gateways and backends:\n\n1. Hostname MUST be used as the SNI to connect to the backend (RFC 6066). 2. Hostname MUST be used for authentication and MUST match the certificate\n   served by the matching backend.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "wellKnownCACertificates": {
                                                             "description": "WellKnownCACertificates specifies whether system CA certificates may be used in the TLS handshake between the gateway and backend pod.\n\nIf WellKnownCACertificates is unspecified or empty (\"\"), then CACertificateRefs must be specified with at least one entry for a valid configuration. Only one of CACertificateRefs or WellKnownCACertificates may be specified, not both. If an implementation does not support the WellKnownCACertificates field or the value supplied is not supported, the Status Conditions on the Policy MUST be updated to include an Accepted: False Condition with Reason: Invalid.\n\nSupport: Implementation-specific",
                                                             "type": "string"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.Gateway": {
                                                     "description": "Gateway represents an instance of a service-traffic handling infrastructure by binding Listeners to a set of IP addresses.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of Gateway.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewaySpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of Gateway.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.GatewayClass": {
                                                     "description": "GatewayClass describes a class of Gateways available to the user for creating Gateway resources.\n\nIt is recommended that this resource be used as a template for Gateways. This means that a Gateway is based on the state of the GatewayClass at the time it was created and changes to the GatewayClass or associated parameters are not propagated down to existing Gateways. This recommendation is intended to limit the blast radius of changes to GatewayClass or associated parameters. If implementations choose to propagate GatewayClass changes to existing Gateways, that MUST be clearly documented by the implementation.\n\nWhenever one or more Gateways are using a GatewayClass, implementations SHOULD add the `gateway-exists-finalizer.gateway.networking.k8s.io` finalizer on the associated GatewayClass. This ensures that a GatewayClass associated with a Gateway is not deleted while in use.\n\nGatewayClass is a Cluster level resource.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of GatewayClass.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayClassSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of GatewayClass.\n\nImplementations MUST populate status on all GatewayClass resources which specify their controller name.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.GatewayClassStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.GatewayClassList": {
                                                     "description": "GatewayClassList contains a list of GatewayClass",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.GatewayClass"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.GatewayList": {
                                                     "description": "GatewayList contains a list of Gateways.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.Gateway"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.HTTPRoute": {
                                                     "description": "HTTPRoute provides a way to route HTTP requests. This includes the capability to match requests by hostname, path, header, or query param. Filters can be used to specify additional processing steps. Backends specify where matching requests should be routed.",
                                                     "type": "object",
                                                     "required": [
                                                         "spec"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of HTTPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteSpec"
                                                         },
                                                         "status": {
                                                             "description": "Status defines the current state of HTTPRoute.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1.HTTPRouteStatus"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.HTTPRouteList": {
                                                     "description": "HTTPRouteList contains a list of HTTPRoute.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.HTTPRoute"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrant": {
                                                     "description": "ReferenceGrant identifies kinds of resources in other namespaces that are trusted to reference the specified kinds of resources in the same namespace as the policy.\n\nEach ReferenceGrant can be used to represent a unique trust relationship. Additional Reference Grants can be used to add to the set of trusted sources of inbound references for the namespace they are defined within.\n\nAll cross-namespace references in Gateway API (with the exception of cross-namespace Gateway-route attachment) require a ReferenceGrant.\n\nReferenceGrant is a form of runtime verification allowing users to assert which cross-namespace object references are permitted. Implementations that support ReferenceGrant MUST NOT permit cross-namespace references which have no grant, and MUST respond to the removal of a grant by revoking the access that the grant allowed.",
                                                     "type": "object",
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ObjectMeta"
                                                         },
                                                         "spec": {
                                                             "description": "Spec defines the desired state of ReferenceGrant.",
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantSpec"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantFrom": {
                                                     "description": "ReferenceGrantFrom describes trusted namespaces and kinds.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind",
                                                         "namespace"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. When empty, the Kubernetes core API group is inferred.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the kind of the referent. Although implementations may support additional resources, the following types are part of the \"Core\" support level for this field.\n\nWhen used to permit a SecretObjectReference:\n\n* Gateway\n\nWhen used to permit a BackendObjectReference:\n\n* GRPCRoute * HTTPRoute * TCPRoute * TLSRoute * UDPRoute",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "namespace": {
                                                             "description": "Namespace is the namespace of the referent.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantList": {
                                                     "description": "ReferenceGrantList contains a list of ReferenceGrant.",
                                                     "type": "object",
                                                     "required": [
                                                         "items"
                                                     ],
                                                     "properties": {
                                                         "apiVersion": {
                                                             "description": "APIVersion defines the versioned schema of this representation of an object. Servers should convert recognized schemas to the latest internal value, and may reject unrecognized values. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#resources",
                                                             "type": "string"
                                                         },
                                                         "items": {
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrant"
                                                             }
                                                         },
                                                         "kind": {
                                                             "description": "Kind is a string value representing the REST resource this object represents. Servers may infer this from the endpoint the client submits requests to. Cannot be updated. In CamelCase. More info: https://git.k8s.io/community/contributors/devel/sig-architecture/api-conventions.md#types-kinds",
                                                             "type": "string"
                                                         },
                                                         "metadata": {
                                                             "default": {},
                                                             "$ref": "#/definitions/io.k8s.apimachinery.pkg.apis.meta.v1.ListMeta"
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantSpec": {
                                                     "description": "ReferenceGrantSpec identifies a cross namespace relationship that is trusted for Gateway API.",
                                                     "type": "object",
                                                     "required": [
                                                         "from",
                                                         "to"
                                                     ],
                                                     "properties": {
                                                         "from": {
                                                             "description": "From describes the trusted namespaces and kinds that can reference the resources described in \"To\". Each entry in this list MUST be considered to be an additional place that references can be valid from, or to put this another way, entries MUST be combined using OR.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantFrom"
                                                             }
                                                         },
                                                         "to": {
                                                             "description": "To describes the resources that may be referenced by the resources described in \"From\". Each entry in this list MUST be considered to be an additional place that references can be valid to, or to put this another way, entries MUST be combined using OR.\n\nSupport: Core",
                                                             "type": "array",
                                                             "items": {
                                                                 "default": {},
                                                                 "$ref": "#/definitions/io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantTo"
                                                             }
                                                         }
                                                     }
                                                 },
                                                 "io.k8s.sigs.gateway-api.apis.v1beta1.ReferenceGrantTo": {
                                                     "description": "ReferenceGrantTo describes what Kinds are allowed as targets of the references.",
                                                     "type": "object",
                                                     "required": [
                                                         "group",
                                                         "kind"
                                                     ],
                                                     "properties": {
                                                         "group": {
                                                             "description": "Group is the group of the referent. When empty, the Kubernetes core API group is inferred.\n\nSupport: Core",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "kind": {
                                                             "description": "Kind is the kind of the referent. Although implementations may support additional resources, the following types are part of the \"Core\" support level for this field:\n\n* Secret when used to permit a SecretObjectReference * Service when used to permit a BackendObjectReference",
                                                             "type": "string",
                                                             "default": ""
                                                         },
                                                         "name": {
                                                             "description": "Name is the name of the referent. When unspecified, this policy refers to all resources of the specified Group and Kind in the local namespace.",
                                                             "type": "string"
                                                         }
                                                     }
                                                 }
                                           }
                                       }
                                       """;
}
