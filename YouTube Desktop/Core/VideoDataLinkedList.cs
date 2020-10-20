using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_Desktop.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VideoData// For dynamic allocation only!
    {
        public VideoData* next;// Ptr to next struct in memory.
        public VideoData* prev;// Ptr to prev struct in memory.
    }
    public sealed class VideoDataLinkedList
    {
        static unsafe public VideoData* AddToList(VideoData* vdList = null)
        {
            VideoData* _ptrArg = vdList;
            VideoData* _ptrTemp = null;
            VideoData* _next = null;
            VideoData* _prev = null;
            if (_ptrArg == null)
            {
                // New list make a new alloc call.
                _ptrArg = (VideoData*)Marshal.AllocHGlobal(sizeof(VideoData));
            }
            else
            {
                // Existing list check for more list ptr's
                if(_ptrArg->next != null)
                {
                    _next = _ptrArg->next;
                }
                if (_ptrArg->prev != null)
                {
                    _prev = _ptrArg->prev;
                }
            }
            _ptrTemp = (VideoData*)Marshal.AllocHGlobal(sizeof(VideoData));
            return _ptrArg;
        }
    }
}