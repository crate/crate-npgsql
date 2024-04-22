from crate.theme.rtd.conf.npgsql import *

# Disable version chooser.
html_context.update({
    "display_version": False,
    "current_version": None,
    "versions": [],
})
