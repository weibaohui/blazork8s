#!/bin/bash

# 定义节点数量
num_nodes=100

# 生成节点定义并写入文件
for ((i = 1; i <= num_nodes; i++)); do
    cat <<EOF >> nodes_definitions.yaml
apiVersion: v1
kind: Node
metadata:
  annotations:
    node.alpha.kubernetes.io/ttl: '0'
    kwok.x-k8s.io/node: fake
  labels:
    beta.kubernetes.io/arch: amd64
    beta.kubernetes.io/os: linux
    kubernetes.io/arch: amd64
    kubernetes.io/hostname: kwok-node-$i
    kubernetes.io/os: linux
    kubernetes.io/role: agent
    node-role.kubernetes.io/agent: ''
    type: kwok
  name: kwok-node-$i
spec:
  taints: # Avoid scheduling actual running pods to fake Node
    - effect: NoSchedule
      key: kwok.x-k8s.io/node
      value: fake  
status:
  allocatable:
    cpu: 32
    memory: 256Gi
    pods: 110
  capacity:
    cpu: 32
    memory: 256Gi
    pods: 110
  nodeInfo:
    architecture: amd64
    bootID: ''
    containerRuntimeVersion: ''
    kernelVersion: ''
    kubeProxyVersion: fake
    kubeletVersion: fake
    machineID: ''
    operatingSystem: linux
    osImage: ''
    systemUUID: ''
  phase: Running
---
EOF
done