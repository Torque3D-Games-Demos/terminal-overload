// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

#include "../../../gl/hlslCompat.glsl"
#include "shadergen:/autogenConditioners.h"

varying vec2 uv0;
uniform sampler2D prepassBuffer;

void main()
{   
   vec3 normal = prepassUncondition( prepassBuffer, uv0 ).xyz;
   gl_FragColor = vec4( ( normal + 1.0 ) * 0.5, 1.0 );
}