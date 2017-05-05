function lateralEye() {
old_x = 0;
old_y = 0;
teamSize = $('.team-grid li').length;
imgSize = $('.team-grid li figure').width();
dir = [];
keys = new Array();
keys['up'] = 5;
keys['down'] = 1;
keys['right'] = 7;
keys['left'] = 3;
keys['enter,space'] = 0;
keys['a'] = 10;
keys['s'] = 3;
keys['d'] = 4;
keys['f'] = 5;
keys['g'] = 6;
keys['h'] = 7;
keys['j'] = 8;
keys['k'] = 9;
keys['l'] = 2;
keys[';'] = 2;
$(document).ready(function() {
    resetDirs = function() {
        dir = [0, -imgSize*8, -imgSize*9, -imgSize*10, -imgSize*6, -imgSize*5, -imgSize*4, -imgSize*3, -imgSize*7, -imgSize*8, 0, -imgSize*1];
    }
    resetDirs();
    $("#wait").hide();
    // $('img.eye').each(function() {
    //     for (direction in dir)
    //     { (new Image()).src = img_base + $(this).attr('rel') + '_' + dir[direction] + '.jpg';
    //     }
    // });
    $("#cont").show();

    for (key in keys)
    {
        eval("$(window).jkey('" + key + "', true, function(){changeAll(" + keys[key] + ");})");
    }
    $('#allfun').click(function() {
        changeAll(10)
    });
    $('#down').click(function() {
        changeAll(1)
    });
    $('#rup').click(function() {
        forFun();
    });
    reverseActions = function() {
        changeAll(1);
        dir = [0, -imgSize*5, -imgSize*4, -imgSize*3, -imgSize*7, -imgSize*8, -imgSize*9, -imgSize*10, -imgSize*6, -imgSize*5, 0, -imgSize*1];
        $("#reverse").hide();
        $('#straighten,#rev').show();
        return false;
    }
    straightenActions = function() {
        resetDirs();
        $('#rev,#straighten').hide();
        $('#reverse').show();
        changeAll(0);
        return false;
    }
    $('#reverse').click(reverseActions);
    $('#straighten').click(straightenActions);
    changeOne = function(element, direction)
    {
        for (var i = 0; i < element.length; ++i) {
            element[i].style.backgroundPositionX = dir[direction] + 'px';
            element[i].style.backgroundPositionY = imgSize + 'px';
        }
    }

    changeAll = function(direction)
    {

        $('.eye').each(function() {
            changeOne($(this), direction);
        });
        return false
    }
    $('.eye').mouseover(function() {
        changeOne($(this), 11);
    });

    setupMouse = function() {
        $(document).mousemove(function(e) {
            if ($('body').hasClass('about-us-body'))
            {
                m_x = e.pageX;
                m_y = e.pageY;
                r_x = m_x - old_x;
                r_y = m_y - old_y;
                dist = Math.sqrt(r_x * r_x + r_y * r_y);
                if (dist > 15)
                {
                    old_x = m_x;
                    old_y = m_y;
                    $('.eye').each(function() {
                        redraw($(this), m_x, m_y);
                    });
                }
            }
        });
    };

    redraw = function(element, mx, my) {
        pos = element.offset();
        half_width = element.width() / 2;
        half_height = element.height() / 2;
        i_x = pos.left + (half_width);
        i_y = pos.top + (half_height)
        rel_x = mx - i_x;
        rel_y = my - i_y;
        dist = Math.sqrt(rel_x * rel_x + rel_y * rel_y);
        if (dist > half_width || dist > half_height)
        {
            angle = Math.acos( - rel_y / dist) * 57.3248;
            sign = (rel_x > 0) ? 1: -1;
            angle = angle * sign;
            p = Math.round(angle / 45) + 5;
            changeOne(element, p);
        }
        else
        {
            changeOne(element, 11);
        }
    }
    setupMouse();
    $('.eye').click(function() {
        changeAll(1);
        changeOne($(this), 10);
    });
});

}

function showTeam(){
    if($('.about-us-body').length != 0) {
        $('section.about-us').addClass('done-animation');
        var teamSize = $('.team-grid li').length;
        $.each($('.team-grid li'), function(i, el){
            setTimeout(function(){
                var figures = $(el).find("figure");
                for (var i = 0; i < figures.length; ++i) {
                    figures[i].style.backgroundPositionX = '0px';
                    figures[i].style.backgroundPositionY = '250px';
                }
                $(el).addClass('visible-member');
            }, 250);
        });
    }
}

$(document).ready(function() {
    showTeam();
    lateralEye();
});