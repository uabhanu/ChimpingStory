#pragma strict

public static class FunctionLibraryJS
{
    //Moves a menu element by the received ammount in time
    public function MoveElementBy(element : Transform, ammount : Vector2, time : float)
    {
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        var startPos : Vector2 = element.position;
        var endPos : Vector2 = element.position;
        endPos += ammount;

        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            element.localPosition = Vector3.Lerp(startPos, endPos, i);

            yield;
        }
    }
    //Rescales the given element to the given scale in time
    public function ScaleTo(element : Transform, endScale : Vector2, time : float)
    {
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        var startScale : Vector2 = element.localScale;

        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            element.localScale = Vector3.Lerp(startScale, endScale, i);

            yield;
        }
    }
    //Sets the active state of the go to state, after time
    public function ChangeEnabledState(go : GameObject, state : boolean, time : float)
    {
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            yield;
        }

        go.SetActive(state);
    }
    //Calls the passed void function with no arguments after delay
    public function CallWithDelay(del : Function, delay : float)
    {
        yield WaitForSeconds(delay);
        del();
    }
    //Calls the passed void function with an int argument after delay
    public function CallWithDelay(del : Function, arg : int, delay : float)
    {
        yield WaitForSeconds(delay);
        del(arg);
    }
    //Fade overlay opacity
    public function FadeScreen(overlay : SpriteRenderer, time : float, to : float)
    {
        //Set the screen fade's color to end in time
        var i : float = 0.0f;
        var rate : float = 1.0f / time;

        var start : Color = overlay.color;
        var end : Color = new Color(start.r, start.g, start.b, to);

        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            overlay.color = Color.Lerp(start, end, i);
            yield;
        }
    }
}
