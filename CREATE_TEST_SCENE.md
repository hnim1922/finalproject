# How to Create a Test Scene for Interactable System

## Step 1: Create a New Scene

1. In Unity Editor, go to **File > New Scene**
2. Choose **3D (Built-in Render Pipeline)** or **3D (URP)** depending on your project
3. Save the scene: **File > Save As**
   - Name it: `TestInteractableScene`
   - Save in: `Assets/Scenes/` (create folder if needed)

## Step 2: Set Up the Environment

1. **Create a Ground Plane:**
   - Right-click in Hierarchy > `3D Object > Plane`
   - Name it: "Ground"
   - Scale: X=10, Y=1, Z=10 (to make it larger)
   - Position: X=0, Y=0, Z=0

2. **Add Lighting:**
   - The scene should have a Directional Light by default
   - If not, Right-click Hierarchy > `Light > Directional Light`

3. **Position Camera:**
   - Select Main Camera
   - Position: X=0, Y=5, Z=-10
   - Rotation: X=20, Y=0, Z=0 (to look down at the scene)

## Step 3: Create Player GameObject

1. **Create Player:**
   - Right-click Hierarchy > `3D Object > Capsule`
   - Name it: **"Player"**

2. **Set Player Tag:**
   - Select "Player" GameObject
   - In Inspector, find **Tag** dropdown
   - If "Player" tag doesn't exist:
     - Click Tag dropdown > `Add Tag...`
     - Click `+` button
     - Name: "Player"
     - Click Save
   - Select "Player" GameObject again
   - Set Tag to **"Player"**

3. **Add Collider:**
   - Select "Player"
   - `Add Component > Physics > Capsule Collider`
   - **DO NOT** check "Is Trigger" (Player needs physical collision)

4. **Add Player Script:**
   - Select "Player"
   - `Add Component > Scripts > Player`
   - In Inspector, configure:
     - **Speed**: 5 (default)
     - **Inventory**: (optional - can add later)

5. **Position Player:**
   - Position: X=0, Y=1, Z=0 (slightly above ground)

## Step 4: Create Interactable GameObject

1. **Create Interactable Object:**
   - Right-click Hierarchy > `3D Object > Cube`
   - Name it: **"TestInteractable"**

2. **Add Collider (Trigger):**
   - Select "TestInteractable"
   - `Add Component > Physics > Box Collider`
   - **CHECK "Is Trigger"** ✓ (This is important!)

3. **Add Interactable Script:**
   - Select "TestInteractable"
   - `Add Component > Scripts > Interactable`
   - The script will automatically ensure the collider is a trigger
   - In Inspector, configure:
     - **Player Tag**: "Player" (default)
     - **Interaction Distance**: 3
     - **Facing Angle Threshold**: 60
     - **Interact UI**: (we'll add this next)

4. **Position Interactable:**
   - Position: X=5, Y=1, Z=0 (away from player)
   - Scale: X=2, Y=2, Z=2 (make it bigger and easier to see)

5. **Make it Visible:**
   - You can change the material color:
     - Create a material: Right-click in Project > `Create > Material`
     - Name it: "InteractableMaterial"
     - Set color to bright (e.g., Red or Yellow)
     - Drag material onto "TestInteractable" GameObject

## Step 5: Create UI for Interaction

### Option A: World Space UI (Recommended - Shows above object)

1. **Create Canvas:**
   - Right-click Hierarchy > `UI > Canvas`
   - Name it: **"InteractCanvas"**
   - Select "InteractCanvas"
   - In Inspector:
     - **Render Mode**: Change to **World Space**
     - **Rect Transform**:
       - Width: 200
       - Height: 50
       - Position: X=0, Y=2, Z=0 (above the object)
     - **Canvas Scaler**:
       - **Scale Factor**: 0.01 (makes UI smaller for 3D space)

2. **Create Button:**
   - Right-click "InteractCanvas" > `UI > Button - TextMeshPro` (or regular Button)
   - Name it: **"InteractButton"**
   - Select "InteractButton"
   - In Inspector, find the **Text (TMP)** child object:
     - Change text to: **"Press E to Interact"**
   - Adjust button size if needed

3. **Attach Canvas to Interactable:**
   - Drag "InteractCanvas" from Hierarchy
   - Drop it as a child of "TestInteractable" in Hierarchy
   - The canvas will now move with the object

4. **Connect UI to Interactable Script:**
   - Select "TestInteractable"
   - In Inspector, find **Interactable** component
   - Find **Interact UI** field
   - Drag "InteractCanvas" from Hierarchy into this field

### Option B: Screen Space UI (Shows on screen)

1. **Create Canvas:**
   - Right-click Hierarchy > `UI > Canvas`
   - Name it: **"ScreenCanvas"**
   - **Render Mode**: Screen Space - Overlay (default)

2. **Create Button:**
   - Right-click "ScreenCanvas" > `UI > Button - TextMeshPro`
   - Name it: **"InteractButton"**
   - Position it in center-bottom of screen
   - Change text to: **"Press E to Interact"**

3. **Connect UI to Interactable Script:**
   - Select "TestInteractable"
   - Drag "InteractButton" into **Interact UI** field

## Step 6: Add Optional Inventory (If Needed)

1. **Create Inventory GameObject:**
   - Right-click Hierarchy > `Create Empty`
   - Name it: **"Inventory"**

2. **Add Inventory Script:**
   - Select "Inventory"
   - `Add Component > Scripts > Inventory`

3. **Connect to Player:**
   - Select "Player"
   - In Inspector, find **Player** component
   - Drag "Inventory" GameObject into **Inventory** field

## Step 7: Test the Scene

1. **Save the Scene:**
   - `File > Save` or `Ctrl+S`

2. **Press Play:**
   - Click the **Play** button (top center)

3. **Test Movement:**
   - Use **W/A/S/D** or **Arrow Keys** to move Player
   - Player should move around the scene

4. **Test Interaction:**
   - Move Player close to "TestInteractable"
   - Rotate Player to face the object (within 60 degrees)
   - UI should appear showing "Press E to Interact"
   - Press **E** key
   - Check Console (Window > General > Console) for debug message: "Interacted with: TestInteractable"

5. **Test UI Visibility:**
   - Face the object → UI appears
   - Turn away from object → UI disappears
   - Move away from object → UI disappears

## Step 8: Add Debug Visuals (Optional)

To make testing easier, you can add visual indicators:

1. **Add Gizmos Script** (for editor visualization):
   - Create empty GameObject: "DebugHelper"
   - Add a custom script to draw gizmos showing interaction range

2. **Add Console Debugging:**
   - The Interactable script already logs to console
   - Open Console window: `Window > General > Console`
   - Watch for messages when entering/exiting range

## Quick Checklist

Before testing, verify:

- [ ] Player has Tag = "Player"
- [ ] Player has Collider (NOT trigger)
- [ ] Player has Player script attached
- [ ] TestInteractable has Collider (IS trigger)
- [ ] TestInteractable has Interactable script attached
- [ ] UI Canvas/Button is connected to Interactable's "Interact UI" field
- [ ] Scene is saved
- [ ] Main Camera is positioned to see the scene

## Troubleshooting

### UI doesn't appear:
- Check Player tag is "Player"
- Check Interactable collider is trigger
- Check UI is assigned to "Interact UI" field
- Check Player is facing the object (within 60° angle)

### Can't interact:
- Check you're pressing E key
- Check Console for errors
- Verify Player is inside trigger collider
- Verify Player is facing object

### Player doesn't move:
- Check Player script is attached
- Check Input Manager settings
- Try different keys (WASD vs Arrow keys)

## Next Steps

Once basic interaction works:
1. Add multiple interactable objects
2. Test different interaction distances
3. Add custom events in OnInteract UnityEvent
4. Test with different UI styles
5. Add sound effects or animations

