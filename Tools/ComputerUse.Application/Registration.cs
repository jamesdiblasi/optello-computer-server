using Microsoft.Extensions.DependencyInjection;

namespace ComputerUse.Application
{
    public static class Registration
    {
        public static IServiceCollection AddComputerUseApplication(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Registration).Assembly));

            return services;
        }
    }
}


/*
 Action_20241022 = Literal[
                "key", //+ 
               "type", //+
         "mouse_move", //+
         "left_click",
    "left_click_drag", //+
        "right_click",
       "middle_click",
       "double_click",
         "screenshot", //+
    "cursor_position", //+
]

Action_20250124 = (
    Action_20241022
    | Literal[
        "left_mouse_down",
          "left_mouse_up",
                 "scroll",
               "hold_key", //+
                   "wait", //????
           "triple_click",
    ]
)
 */