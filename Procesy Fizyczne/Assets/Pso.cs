using UnityEngine;
using System.Collections;

public class Pso : MonoBehaviour
{
    float pBestValue;
    Vector3 pBestPos;
    Vector3 velocity;

    float c1 = 1.49678f;
    float c2 = 1.49678f;

    static GameObject player;
    Rigidbody rb;

    public static Vector3 globalPositionBest;
    public static float globalValueBest = 9999999.0f;
    
    float resolutionLength = Terrain.activeTerrain.terrainData.size.x;
    float resolutionWidth = Terrain.activeTerrain.terrainData.size.z;

    static int counter = 0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        randomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > 10)
        {
            counter = 0;
            globalValueBest = 999;
        }
        Vector3 position = rb.transform.position;
        Vector3 newVelocity;
        Vector3 newPosition;

        float distance = grosen(position);

        if (distance < pBestValue)
        {
            pBestPos = position;
            pBestValue = distance;
        }
        if (distance < globalValueBest)
        {
            globalPositionBest = position;
            globalValueBest = distance;
        }
        float c3 = 0.73f;
        newVelocity.x = velocity.x + c1 * Random.value * (pBestPos.x - position.x) +
            c2 * Random.value * (globalPositionBest.x - position.x);
        newVelocity.x = newVelocity.x * c3;
        newPosition.x = position.x + newVelocity.x;

        newVelocity.y = velocity.y + c1 * Random.value * (pBestPos.y - position.y) +
            c2 * Random.value * (globalPositionBest.y - position.y);
        newVelocity.y = newVelocity.y * c3;
        newPosition.y = position.y + newVelocity.y;

        newVelocity.z = velocity.z + c1 * Random.value * (pBestPos.z - position.z) +
            c2 * Random.value * (globalPositionBest.z - position.z);
        newVelocity.z = newVelocity.z * c3;
        newPosition.z = position.z + newVelocity.z;

        Debug.Log(newVelocity.x+" "+newVelocity.y+" "+newVelocity.z);

         if ( newPosition.x >= 0 && newPosition.x <= resolutionLength && newPosition.z >= 0 && newPosition.z <= resolutionWidth)
        {
             velocity = newVelocity;
             //rb.AddForce(newPosition);
             rb.transform.position = newPosition;
        }
        else
        {
            randomPosition();
        }

         counter++;
    }


    float grosen(Vector3 pos)
    {
        return Mathf.Sqrt(Mathf.Pow(pos.x - player.transform.position.x,2) + 
                Mathf.Pow(pos.y - player.transform.position.y, 2) + 
                Mathf.Pow(pos.z - player.transform.position.z, 2));
    }

    void randomPosition()
    {
        rb.transform.position = new Vector3(resolutionLength * Random.value, 20.0f, resolutionWidth * Random.value);

        velocity.x = (resolutionLength / 100.0f) * (1.0f - 2.0f * Random.value);
        //velocity.y = (resolution / 100.0f) * (1.0f - 2.0f * Random.value);
        velocity.z = (resolutionWidth / 100.0f) * (1.0f - 2.0f * Random.value);

        pBestPos.x = rb.transform.position.x;
        pBestPos.y = rb.transform.position.y;
        pBestPos.z = rb.transform.position.z;

        pBestValue = grosen(rb.transform.position);
    }
}


//int N = 2; //number of dim
//int RANGE = 10000;
//float* GLOBALpos = (float*)malloc(N * sizeof(float));
//int MAXx = RANGE;
//int MINx = -RANGE;
//int MAXy = RANGE;
//int MINy = -RANGE;
//float c1 = 1.49678;
//float c2 = 1.49678;



//struct bird
//{
//    int number;
//    float* pos;
//    float* v;
//    float* pbest;
//    float pbestValue;
//    float pbesterror;
//};

//int main(int argc, char* argv[])
//{
//    srand(time(NULL));
//    int counter = 0;
//    int j;
//    bird birds[10];
//    bool breakAlert = false;
//    for (j = 0; j < 10; j++)
//    {
//        birds[j].number = j;
//        birds[j].pos = (float*)malloc(N * sizeof(float));
//        birds[j].pos[0] = MINx + (MAXx - MINx) * (float)rand() / (float)RAND_MAX;
//        birds[j].pos[1] = MINy + (MAXy - MINy) * (float)rand() / (float)RAND_MAX;
//        birds[j].v = (float*)malloc(2 * sizeof(float));
//        birds[j].v[0] = ((MAXx - MINx) / 100.0) * (1.0 - 2.0 * (float)rand() / (float)RAND_MAX);
//        birds[j].v[1] = ((MAXy - MINy) / 100.0) * (1.0 - 2.0 * (float)rand() / (float)RAND_MAX);
//        birds[j].pbest = (float*)malloc(N * sizeof(float));
//        birds[j].pbest[0] = birds[j].pos[0];
//        birds[j].pbest[1] = birds[j].pos[1];
//        birds[j].pbestValue = grosen(birds[j].pos, N);
//    }
//    while (true)
//    //for (int ii = 0; ii < 5; ii++)
//    {
//        counter++;
//        printf("\n %d \n", counter);
//        for (j = 0; j < 10; j++)
//        {
//            printf("X: %.15f \t Y: %.15f \t value: %.15f \n", birds[j].pos[0], birds[j].pos[1], birds[j].pbestValue);
//            float temp = grosen(birds[j].pos, 2);
//            if (temp < birds[j].pbestValue)
//            {
//                birds[j].pbest = birds[j].pos;
//                birds[j].pbestValue = temp;
//            }
//            if (temp < grosen(GLOBALpos, 2))
//            {
//                GLOBALpos = birds[j].pos;
//            }

//            //if( temp <= 0.00005)
//            if (temp == 0)
//            {
//                breakAlert = true;
//                break;
//            }

//            float x1 = birds[j].v[0] + c1 * (float)rand() / (float)RAND_MAX * (birds[j].pbest[0] - birds[j].pos[0]) +
//                c2 * (float)rand() / (float)RAND_MAX * (GLOBALpos[0] - birds[j].pos[0]);
//            x1 = x1 * 0.73;
//            float y1 = birds[j].pos[0] + x1;


//            float x2 = birds[j].v[1] + c1 * (float)rand() / (float)RAND_MAX * (birds[j].pbest[1] - birds[j].pos[1]) +
//                c2 * (float)rand() / (float)RAND_MAX * (GLOBALpos[1] - birds[j].pos[1]);
//            x2 = x2 * 0.73;
//            float y2 = birds[j].pos[1] + x2;

//            if (y1 >= MINx && y1 <= MAXx && y2 >= MINy && y2 <= MAXy)
//            {
//                birds[j].v[0] = x1;
//                birds[j].pos[0] = y1;
//                birds[j].v[1] = x2;
//                birds[j].pos[1] = y2;
//            }
//            else
//            {
//                birds[j].pos[0] = MINx + (MAXx - MINx) * (float)rand() / (float)RAND_MAX;
//                birds[j].pos[1] = MINy + (MAXy - MINy) * (float)rand() / (float)RAND_MAX;
//                birds[j].v[0] = ((MAXx - MINx) / 100.0) * (1.0 - 2.0 * (float)rand() / (float)RAND_MAX);
//                birds[j].v[1] = ((MAXy - MINy) / 100.0) * (1.0 - 2.0 * (float)rand() / (float)RAND_MAX);
//            }
//        }
//        if (breakAlert)
//            break;
//    }
//    printf("\n\nFINISH: X: %.15f Y: %.15f value: %.15f \n", GLOBALpos[0], GLOBALpos[1], grosen(GLOBALpos, N));
//    return 0;
//}

//float grosen(float* x, int n)
//{
//    float f;
//    int i;
//    f = 0.0;
//    for (i = 0; i < n - 1; i++)
//    {
//        f = f + 100.0 * powf(x[i + 1] - x[i], 2.0) + powf(1.0 - x[i], 2.0);
//    }
//    return f;
//}