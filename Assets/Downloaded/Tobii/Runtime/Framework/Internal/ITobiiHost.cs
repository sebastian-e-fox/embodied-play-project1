﻿//-----------------------------------------------------------------------
// Copyright 2016 Tobii AB (publ). All rights reserved.
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Tobii.G2OM;
using UnityEngine;

namespace Tobii.Gaming.Internal
{
	internal interface ITobiiHost
	{
		/// <summary>
		/// Checks if Tobii software is installed and device is connected,
		/// configured and running.
		/// </summary>
		bool IsConnected { get; }

		/// <summary>
		/// Gets the GazeFocus handler.
		/// </summary>
		List<FocusedCandidate> GetFocusedObjects();

		/// <summary>
		/// Gets information about the eye-tracked display monitor.
		/// </summary>
		DisplayInfo DisplayInfo { get; }

		/// <summary>
		/// Gets the engine state: User presence.
		/// </summary>
		UserPresence UserPresence { get; }

		/// <summary>
		/// Gets Extended View Yaw & Pitch
		/// </summary>
		TobiiExtendedViewAngles ExtendedViewAngles { get; }

		/// <summary>
		/// Controls the behaviour of Extended View. Passed to underlying API.
		/// </summary>
		TobiiExtendedViewSettings ExtendedViewSettings { get; set; }

		/// <summary>
		/// Returns a value indicating whether the host has been initialized.
		/// </summary>
		bool IsInitialized { get; }

		/// <summary>
		/// Gets information about the Game View window.
		/// </summary>
		GameViewInfo GameViewInfo { get; }

		/// <summary>
		/// Gets a provider of gaze point data using default data processing.
		/// </summary>
		/// <returns>The data provider.</returns>
		IDataProvider<GazePoint> GetGazePointDataProvider();

		/// <summary>
		/// Gets a provider of head pose data.
		/// See <see cref="IDataProvider{T}"/>.
		/// </summary>
		/// <returns>The data provider.</returns>
		IDataProvider<HeadPose> GetHeadPoseDataProvider();

		/// <summary>
		/// Shuts down the host.
		/// </summary>
		void Shutdown();

		/// <summary>
		/// Sets the camera that defines the user's current view point.
		/// </summary>
		void SetCurrentUserViewPointCamera(Camera camera);
		
		/// <summary>
		/// Starts G2OM and the gaze data provider.
		/// </summary>
		bool Initialize(TobiiSettings settings);
	}
}
