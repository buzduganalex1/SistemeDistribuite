# Hyperledger Fabric: A Distributed Operating System for Permissioned Blockchains


Open source system used for deploying and operating permissioned blockchains.

## What is a permissioned blockchain?
Permissioned blockchains, on the other hand, run a blockchain among a set of known, identified participants.

## Other info
- It is quite popular used in over 400 prototypes

- First truly extensible blockchain system for running distributed applications.

- Can use normal programming languages and not cryptocurrency specific ones

- On normal platforms smart contracts can be written only in currency specific language.

- Consensus protocol - validation of transactions


## How blockchains are different from SMR
- not only one, but many distributed applications run concurrently;
- applications may be deployed dynamically and by anyone
- the application code is untrusted, potentially even malicious. 
 
 Blockchains implement so-called **Active replication** a protocol for consensus or atomic broadcast
first orders the transactions and propagates them to all peers;
and second, each peer executes the transactions sequentially.
We call this the **Order-execute architecture**

## Reasons for Hyperledger Fabric
- Consensus is hard-coded within the platform, which contradicts
the well-established understanding that there is
no “one-size-fits-all” (BFT) consensus protocol [33];

- The trust model of transaction validation is determined
by the consensus protocol and cannot be adapted to the
requirements of the smart contract;

- Smart contracts must be written in a fixed, non-standard,
or domain-specific language, which hinders wide-spread
adoption and may lead to programming errors;

- The sequential execution of all transactions by all peers
limits performance, and complex measures are needed
to prevent denial-of-service attacks against the platform
originating from untrusted contracts (such as accounting
for runtime with “gas” in Ethereum);

- Transactions must be deterministic, which can be difficult
to ensure programmatically;

- Every smart contract runs on all peers, which is at odds
with confidentiality, and prohibits the dissemination of
contract code and state to a subset of peers

## AppFabric architecture

The architecture of Fabric follows a novel **execute-order-validate
paradigm** for distributed execution of untrusted
code in an untrusted environment. 

It separates the transaction
flow into three steps, which may be run on different
entities in the system:

- executing a transaction and checking its correctness, thereby endorsing it (corresponding to “transaction validation” in other blockchains)

- ordering through a consensus protocol, irrespective of transaction semantics

- transaction validation per applicationspecific trust assumptions, which also prevents race conditions due to concurrency.

## Hybrid replication
Mixes passive and active replication in the **Byzantine model**, with the
**execute-order-validate** paradigm.

They resolve the issues mentioned and make Fabric a scalable system for permissioned
blockchains supporting flexible trust assumptions.
To implement this architecture, Fabric contains modular
building blocks for each of the following components:

- **Ordering service** 
    * An ordering service atomically broadcasts
        state updates to peers and establishes consensus on the order of transactions. It has been implemented with Apache Kafka/ZooKeeper

- **Identity and membership**
    * A membership service provider is responsible for associating peers with cryptographic identities. It maintains the permissioned nature of Fabric

- **Scalable dissemination**
    * An optional peer-to-peer gossip
    service disseminates the blocks output by ordering service to all peers.

- **Smart-contract execution**
    * Smart contracts in Fabric run within a container environment for isolation. They can be written in standard programming languages but do not have direct access to the ledger state.

- **Ledger maintenance**
    * Each peer locally maintains the ledger in the form of the append-only blockchain and as a snapshot of the most recent state in a key-value store (KVS). The KVS can be implemented by standard libraries, such
    as LevelDB or Apache CouchDB.
