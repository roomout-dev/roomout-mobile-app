//-----------------------------------------------------------------------
// <copyright file="LocalPlayerController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.CloudAnchors
{
    using GoogleARCore.Examples.ObjectManipulation;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;

    /// <summary>
    /// Local player controller. Handles the spawning of the networked Game Objects.
    /// </summary>
#pragma warning disable 618
    public class LocalPlayerController : NetworkBehaviour
#pragma warning restore 618
    {
        /// <summary>
        /// The Treasure Chest that will represent networked objects in the scene.
        /// </summary>
        public GameObject TreasureChestPrefab;

        /// <summary>
        /// The GoldenKey that will represent networked objects in the scene.
        /// </summary>
        public GameObject GoldenKeyPrefab;

        /// <summary>
        /// Manipulator prefab to attach placed objects to.
        /// </summary>
        public GameObject ManipulatorPrefab;

        /// <summary>
        /// The Anchor model that will represent the anchor in the scene.
        /// </summary>
        public GameObject AnchorPrefab;

        public GameObject TimerPrefab;

        /// <summary>
        /// Manipulator prefab to attach the Golden Key.
        /// </summary>
        public GameObject ManipulatorGoldenKeyPrefab;

        // Handle the chest setup
        private Vector3 ChestPosition;
        private Quaternion ChestRotation;
        private Transform ChestAnchor;

        /// <summary>
        /// The Unity OnStartLocalPlayer() method.
        /// </summary>
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            // A Name is provided to the Game Object so it can be found by other Scripts, since this
            // is instantiated as a prefab in the scene.
            gameObject.name = "LocalPlayer";
        }

        /// <summary>
        /// Will spawn the origin anchor and host the Cloud Anchor. Must be called by the host.
        /// </summary>
        /// <param name="position">Position of the object to be instantiated.</param>
        /// <param name="rotation">Rotation of the object to be instantiated.</param>
        /// <param name="anchor">The ARCore Anchor to be hosted.</param>
        public void SpawnAnchor(Vector3 position, Quaternion rotation, Component anchor)
        {
            // Instantiate Anchor model at the hit pose.
            var anchorObject = Instantiate(AnchorPrefab, position, rotation);

            // Anchor must be hosted in the device.
            anchorObject.GetComponent<AnchorController>().HostLastPlacedAnchor(anchor);

            // Host can spawn directly without using a Command because the server is running in this
            // instance.
#pragma warning disable 618
            NetworkServer.Spawn(anchorObject);
#pragma warning restore 618

            // Prepare Treasure Chest informations for later spawn
            ChestPosition = position;
            ChestRotation = rotation;
            ChestAnchor = (Transform) anchor;
        }

        /// <summary>
        /// Will spawn the Treasure Chest after the Cloud Anchor is set
        /// </summary>
        public void SpawnTreasureChest()
        {
            // Spawn the treasure chest
            // Instantiate game object at the hit pose.
            var gameObject = Instantiate(TreasureChestPrefab, ChestPosition, ChestRotation);
            gameObject.transform.Rotate(-90f, ChestRotation.y, -180f);

            // Instantiate manipulator.
            var manipulator =
                Instantiate(ManipulatorPrefab, ChestPosition, ChestRotation);

            // Make game object a child of the manipulator.
            gameObject.transform.parent = manipulator.transform;

            // Make manipulator a child of the anchor.
            manipulator.transform.parent = ChestAnchor;

#pragma warning disable 618
            // Spawn the object in all clients.
            NetworkServer.Spawn(gameObject);
#pragma warning restore 618

            // Key handling
            Vector3 keyPosition = ChestPosition;
            keyPosition.x += 0.5f;

            // Spawn the Golden key
            // Instantiate game object at the hit pose.
            var goldenKey = Instantiate(GoldenKeyPrefab, keyPosition, ChestRotation);

            // Instantiate manipulator.
            var manipulatorGoldenKey = Instantiate(ManipulatorGoldenKeyPrefab, keyPosition, ChestRotation);

            // Make manipulator a child of the anchor.
            goldenKey.transform.parent = manipulatorGoldenKey.transform;

#pragma warning disable 618
            NetworkServer.Spawn(goldenKey);
#pragma warning restore 618

            // Set the GameObject to the Event listener
            TreasureHandler treasureHandler = GameObject.Find("TreasureHandler").GetComponent<TreasureHandler>();
            treasureHandler.SetPrefab(gameObject);
            treasureHandler.SetGoldenKey(GoldenKeyPrefab);
        }

        /// <summary>
        /// Start the countdown
        /// </summary>
        public void StartTimer()
        {
            var timer = Instantiate(TimerPrefab);

            // GameObject timer = GameObject.Find("TimerHandler");
            timer.SetActive(true);

            // Set the text component
            timer.GetComponent<TimerController>().SetText(GameObject.Find("TextTime").GetComponent<Text>());

            // Start the Timer
            timer.GetComponent<TimerController>().StartTimer();

            // Spawn the GameObject timer
#pragma warning disable 618
            NetworkServer.Spawn(timer);
#pragma warning restore 618
        }

        /// <summary>
        /// A command run on the server that will spawn the Star prefab in all clients.
        /// </summary>
        /// <param name="position">Position of the object to be instantiated.</param>
        /// <param name="rotation">Rotation of the object to be instantiated.</param>
#pragma warning disable 618
        [Command]
#pragma warning restore 618
        public void CmdSpawnStar(Vector3 position, Quaternion rotation)
        {
            // Instantiate Star model at the hit pose.
            // var starObject = Instantiate(StarPrefab, position, rotation);

            // Spawn the object in all clients.
#pragma warning disable 618
            // NetworkServer.Spawn(starObject);
#pragma warning restore 618
        }
    }
}
