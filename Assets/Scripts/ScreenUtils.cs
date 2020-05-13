using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides screen utilities.
/// </summary>
public static class ScreenUtils
{
    #region Fields

    // Saved to support resolution changes.
    static int screenWidth;
    static int screenHeight;

    // Cached for efficient boundary checking.
    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the left edge of the screen in world coordinates.
    /// </summary>
    /// <value>Left edge of the screen</value>
    public static float ScreenLeft
    {
        get
        {
            CheckScreenSizeChanged();
            return screenLeft;
        }
    }

    /// <summary>
    /// Gets the right edge of the screen in world coordinates.
    /// </summary>
    /// <value>Right edge of the screen</value>
    public static float ScreenRight
    {
        get
        {
            CheckScreenSizeChanged();
            return screenRight;
        }
    }

    /// <summary>
    /// Gets the top edge of the screen in world coordinates.
    /// </summary>
    /// <value>Top edge of the screen</value>
    public static float ScreenTop
    {
        get
        {
            CheckScreenSizeChanged();
            return screenTop;
        }
    }

    /// <summary>
    /// Gets the bottom edge of the screen in world coordinates.
    /// </summary>
    /// <value>Bottom edge of the screen</value>
    public static float ScreenBottom
    {
        get 
        {
            CheckScreenSizeChanged();
            return screenBottom; 
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the screen utilities.
    /// </summary>
    public static void Initialize()
    {
        // Save to support resolution changes.
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        // Save screen edges in world coordinates.
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(
            screenWidth, screenHeight, screenZ);
        Vector3 lowerLeftCornerWorld =
            Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }

    /// <summary>
    /// Checks for screen size change.
    /// </summary>
    static void CheckScreenSizeChanged()
    {
        if (screenWidth != Screen.width ||
            screenHeight != Screen.height)
        {
            Initialize();
        }
    }

    #endregion
}
