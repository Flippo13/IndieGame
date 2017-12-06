var flickerSpeed : float = 0.07;
private var randomizer : int = 0;
 
while (true)
{
    if (randomizer == 0) {
        GetComponent.<Light>().enabled = false;
    }
    else GetComponent.<Light>().enabled = true;
    randomizer = Random.Range (0, 1.1);
    yield WaitForSeconds(flickerSpeed);
}