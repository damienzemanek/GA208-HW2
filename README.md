# HW2
## Devlog
<hr>

#### PROMPT:
Write about how the plan you drew for the MG2 break-down activity connects to the code you wrote.
Cite specific class names and method names in the code and GameObjects in your Unity Scene.
<hr>

### Objects Overview

The plan I drew for the MG-2 Breakdown activity connects to the code I wrote through its GameObject, method, 
and variable separation and indication. I was able to prepare GameObjects from my breakdown like:
- Floor
- Player
- Coin
- CoinSpawner

### `Penguin` Object & `Player` Script: Jumping & Grounded
The first object I decided to work on was the `Penguin` Object. I decided to work on its `Jump` action first.
In order to do this I created the script `Player` and attached it to my penguin GameObject in scene called `Player`
The attributes the `Penguin` Object had in my breakdown included `horizSpeed` & `vertMult` with some member methods like `OnTriggerEnter2D`.
After going in Editor I realised the `Penguin`'s implemention wouldn't actually move, otherwise
I would need the background to infinite scroll. So I removed any horizontal movement/ movementSpeed. The `vertMult` attribute
turned into `jumpScalar` and was used in the Rb2D's AddForce Impulse method.

For the grounded check I had some previous code from my extension method library I've been developing. This extension method class is
called [PhysEX](https://github.com/damienzemanek/ARPG/blob/main/Assets/Import/EMILtools-Private/Extensions/PhysEX.cs) <--- recent PhysEX version.
I took the `IsGrounded2D` check and stuck it in this project and it worked immedietly. Now the player's full jumping action worked according to my breakdown. 
This had the effect of adding a struct called `GroundedSettings` as another attribute/variable for the `Player`.

### Dependancy Management

The game was too simple to prepare any dependancy chains or prepare any dependancy management so direct references are fine.
No scripts reference eachother and are independant (Intentional), for example:
- Initially I had my coin collecting functionality on my `Player` script, but I realized I should probably have a way to remove coins that pass the player, in order to accomplish this I removed the collect functionality from the player, put it on a `Collect` script, and gave it a bool to check if this collection event counts towards the point total. Making use of this, I put this `Collect` bounds behind and off screen of the player so that any coins not caught by the player will be caught by this extra `Collect` bounds, and not be added to the point total.

### `Coin` movement, `CoinSpawner` and the `CoinSpawner` Script

The `Coin` Object in my breakdown only corrosponded to a coin prefab in the Editor, I did not add any `Coin` script.
So for the movement I decided that batch moving the coins would be the most efficient instead of making a new Component script for each of the coins.
This way the coins are more lightweight. (Although it doesn't really matter the game is so simple anyway but I think it was a fun excersise).
I went through a couple versions of the coins moving, but eventually landed on making the batcher check for removals and back indexing:

```csharp
void MoveCoins()
{
    for (int i = 0; i < spawnedCoins.Count; i++)
    {
        if(spawnedCoins[i] == null) { spawnedCoins.RemoveAt(i); i--; }
        else spawnedCoins[i].transform.Translate(Vector3.left * moveSpeed);
    }
}
```
In my original breakdown I indicated that the coins would be moving, however this changed to what I just mentioned due to the design descision


### `Spawn()` on the `CoinSpawner`

To cover the action `Spawn` on my breakdown's Object `CoinSpawner` I started a `CoinSpawner` script.
I did not indicate any attributes for the `CoinSpawner` script in my Breakdown because I was not clear on how I wanted to implement it until the player and coins were decided on.
After deciding how I wanted the Spawn to occur, I used the attributes `maxY` `minY` `spawnDelay` `moveSpeed` and `coinPrefab` as my script variables.
These were used to implement the breakdown's action `SpawnCoin` as `Spawn()`


```csharp
void Spawn()
{
    var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
    coin.transform.position = coin.transform.position.With(y: Random.Range(minY, maxY));
    spawnedCoins.Add(coin);
}
```
An interesting thing that I used is my Vector3 extension method class [Vector3EX](https://github.com/damienzemanek/ARPG/blob/main/Assets/Import/EMILtools-Private/Extensions/Vector3EX.cs) and especially the method `With():`

```csharp
public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    => new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
```
This allows me to inline param edit the individual vector floats.

Thanks for reading! :)

### [MG-2 Game Build 0.0.1](https://starnightstudios.itch.io/ga208-hw2)


## Open-Source Assets
If you added any other outside assets, list them here!
- [Sprout Lands sprite asset pack](https://cupnooble.itch.io/sprout-lands-asset-pack) - rabbit and item sprites
- [Pixel Penguin 32x32 Asset pack](https://legends-games.itch.io/pixel-penguin-32x32-asset-pack) - penguin sprites
- [Coins 2D](https://artist2d3d.itch.io/2d) - coin sprites