using GXPEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReactionParticle : Sprite
{
    float vx, vy; // horizontal and vertical velocity
    float startScale = 1;
    float endScale = 1;

    // Life time variables:

    int totalLifeTimeMs;
    int currentLifeTimeMs = 0;

    public ReactionParticle(string filename, int lifeTimeMs) : base(filename, true, false)
    { // no colliders for particles!
        totalLifeTimeMs = lifeTimeMs;
        SetOrigin(width / 2, height / 2);
    }

    public ReactionParticle SetScale(float start, float end)
    {
        startScale = start;
        endScale = end;
        scale = start;
        return this; // allows for chaining
    }

    public ReactionParticle SetVelocity(float velX, float velY)
    {
        vx = velX;
        vy = velY;
        return this; // allows for chaining
    }

    void Update()
    {
        // Keep track of this particle's life time:
        currentLifeTimeMs += Time.deltaTime;
        // Make a parameter that goes from 0 to 1 throughout the lifetime of the particle:
        float t = Mathf.Clamp(1f * currentLifeTimeMs / totalLifeTimeMs, 0, 1);

        // ---- Interpolate parameters, using linear interpolation (alternatively, you can use tweening curves here too!):

        // interpolate scale:
        scale = startScale * (1 - t) + endScale * t;

        // Move:
        x += vx;
        y += vy;

        if (currentLifeTimeMs >= totalLifeTimeMs)
        {
            Destroy();
        }
    }
}
