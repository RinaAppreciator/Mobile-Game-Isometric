using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerJump : MonoBehaviour
{

    //player variables
    private SpriteRenderer spriteRenderer;
    bool isGrounded;
    bool isJumping;
    float jumpVelocity = 5f;
    float gravity = -9.8f;
    float verticalSpeed = 0f;
    float fakeZ = 0f;

    //architecture variables

    public static PlayerJump instance;
    public Tilemap tilemap;  // Assigned dynamically


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // tilemap = LevelManager.instance.currentTilemap; // Get initial tilemap
    }

 

    // Update is called once per frame
    void Update()
    {
        if (tilemap == null) return; // Prevent errors if no tilemap is assigned

        float groundHeight = GetGroundHeight(transform.position);
        bool isOnPlatform = tilemap.HasTile(tilemap.WorldToCell(transform.position));

        bool isFalling = !isOnPlatform && fakeZ <= 0;

        if (!isFalling && fakeZ <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            verticalSpeed = jumpVelocity;
        }

        if (fakeZ > 0 || verticalSpeed > 0 || isFalling)
        {
            fakeZ += verticalSpeed * Time.deltaTime;
            verticalSpeed += gravity * Time.deltaTime;
        }

        if (fakeZ <= 0)
        {
            if (isOnPlatform)
            {
                fakeZ = 0;
                verticalSpeed = 0;
                transform.position = new Vector3(transform.position.x, groundHeight, transform.position.z);
            }
            else
            {
                fakeZ = 0.01f; // Keep falling
            }
        }

        spriteRenderer.sortingOrder = Mathf.RoundToInt((-transform.position.y + fakeZ) * 100);
    }

    public void UpdateTilemap(Tilemap newTilemap)
    {
        tilemap = newTilemap;
    }

    float GetGroundHeight(Vector2 position)
    {
        Vector3Int tilePos = tilemap.WorldToCell(position);
        TileBase tile = tilemap.GetTile(tilePos);

        if (tile != null)
        {
            return tilePos.y;
        }
        return -1f;
    }




}

